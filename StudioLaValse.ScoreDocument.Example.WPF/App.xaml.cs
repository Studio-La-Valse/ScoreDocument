using StudioLaValse.Drawable.WPF.Painters;
using StudioLaValse.Drawable.WPF.UserControls;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using System.Windows;
using StudioLaValse.Geometry;
using StudioLaValse.Drawable;
using StudioLaValse.Drawable.Extensions;
using StudioLaValse.Drawable.Interaction.Extensions;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Drawable.Interaction.UserInput;
using System;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Drawable.Models;

namespace StudioLaValse.ScoreDocument.Example.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var notifyEntityChanged = SceneManager<IUniqueScoreElement>.CreateObservable();
            var selectionManager = SelectionManager<IUniqueScoreElement>.CreateDefault().OnChangedNotify(notifyEntityChanged);

            var kortjakje = System.Text.Encoding.Default.GetString(WPF.Resources.Kortjakje);
            var score = ScoreBuilder.CreateDefault(string.Empty, string.Empty)
                .Edit(score =>
                {
                    score.AddInstrumentRibbon(Instrument.Violin);
                    score.AddInstrumentRibbon(Instrument.Piano);
                })
                .Edit(score =>
                {
                    for (int i = 0; i < 21; i++)
                    {
                        score.AppendScoreMeasure(new TimeSignature(4, 4));
                        var measure = score.EditScoreMeasure(i);
                        measure.ApplyLayout(new ScoreMeasureLayout()
                        {
                            KeySignature = new(Step.C, MajorOrMinor.Major),
                            IsNewSystem = i % 6 == 0
                        });
                    }
                })
                .Build();

            var canvas = new WindowsDrawingContextUserControl();
            var canvasPainter = new WindowsDrawingContextBitmapPainter(canvas);
            var selectionBorder = new DragSelectionUserControl();
            var selectionBorderObservable = ISelectionBorder.CreateObservable();
            var selectionBorderObserver = selectionBorder.CreateObserver(canvas);
            selectionBorderObservable.Subscribe(selectionBorderObserver);

            var noteFactory = new VisualNoteFactory(selectionManager);
            var restFactory = new VisualRestFactory(selectionManager);
            var beamBuilder = new VisualBeamBuilder();
            var noteGroupFactory = new VisualNoteGroupFactory(noteFactory, restFactory, beamBuilder);
            var staffMeasusureFactory = new VisualStaffMeasureFactory(selectionManager, noteGroupFactory);
            var systemMeasureFactory = new VisualSystemMeasureFactory(selectionManager, staffMeasusureFactory);
            var staffSystemFactory = new VisualStaffSystemFactory(systemMeasureFactory, selectionManager);
            var sceneFactory = new PageViewSceneFactory(staffSystemFactory, PageSize.A4, 20, 30, ColorARGB.Black, ColorARGB.White);
            var origin = new VisualScoreDocumentScene(sceneFactory, score);
            var sceneManager = new SceneManager<IUniqueScoreElement>(origin)
                .WithBackground(ColorARGB.White)
                .WithRerender(canvasPainter);

            var entityObserver = sceneManager.CreateObserver(canvasPainter);
            notifyEntityChanged.Subscribe(entityObserver);

            var pipe = Pipeline.DoNothing()
                .InterceptKeys(selectionManager, out var _selectionManager)
                .ThenHandleDefaultMouseInteraction(sceneManager.VisualParents, notifyEntityChanged)
                .ThenHandleMouseHover(sceneManager.VisualParents, notifyEntityChanged)
                .ThenHandleDefaultClick(sceneManager.VisualParents, _selectionManager)
                .ThenHandleSelectionBorder(sceneManager.VisualParents, _selectionManager, selectionBorderObservable, notifyEntityChanged)
                .ThenHandleTransformations(_selectionManager, sceneManager.VisualParents, notifyEntityChanged)
                .ThenRender(notifyEntityChanged);
            canvas.EnableZoom();
            canvas.EnablePan();
            canvas.Subscribe(pipe);

            var window = new MainWindow(canvas, selectionBorder);
            window.Show();
        }
    }
}
