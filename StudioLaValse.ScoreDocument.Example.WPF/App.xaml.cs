using StudioLaValse.Drawable;
using StudioLaValse.Drawable.Extensions;
using StudioLaValse.Drawable.Interaction.Extensions;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Drawable.Interaction.UserInput;
using StudioLaValse.Drawable.WPF.Painters;
using StudioLaValse.Drawable.WPF.UserControls;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.MusicXml;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Layout.Elements;
using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using System.Linq;

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

            var notifyEntityChanged = SceneManager<IUniqueScoreElement, int>.CreateObservable();
            var selectionManager = SelectionManager<IUniqueScoreElement>.CreateDefault().OnChangedNotify(notifyEntityChanged);

            IScoreDocumentReader? score;
            using (var ms = new MemoryStream(WPF.Resources.Kortjakje))
            {
                var document = XDocument.Load(ms);
                score = MusicXmlScoreDocumentBuilder.Create(document).Build();
            }
            
            var canvas = new WindowsDrawingContextUserControl();
            var canvasPainter = new WindowsDrawingContextBitmapPainter(canvas);
            var selectionBorder = new DragSelectionUserControl();
            var selectionBorderObservable = ISelectionBorder.CreateObservable();
            var selectionBorderObserver = selectionBorder.CreateObserver(canvas);
            selectionBorderObservable.Subscribe(selectionBorderObserver);

            var pageSize = PageSize.A4;
            var scoreDocumentStyle = ScoreDocumentStyle.Create(style =>
            {
                style.MeasureBlockStyle = (m) => new ()
                {
                    StemLength = 4,
                    BeamAngle = 0
                };
            });
            var scoreLayoutDictionary = new ScoreLayoutDictionary(scoreDocumentStyle);
            scoreLayoutDictionary.Apply(score, new ScoreDocumentLayout()
            {
                BreakSystem = (m, s) => m.Select((m, n) => scoreLayoutDictionary.GetOrCreate(m).Width).Sum() > s.PageSize.Width
            });
            var noteFactory = new VisualNoteFactory(selectionManager, scoreLayoutDictionary);
            var restFactory = new VisualRestFactory(selectionManager);
            var noteGroupFactory = new VisualNoteGroupFactory(noteFactory, restFactory, scoreLayoutDictionary);
            var staffMeasusureFactory = new VisualStaffMeasureFactory(selectionManager, noteGroupFactory, scoreLayoutDictionary);
            var systemMeasureFactory = new VisualSystemMeasureFactory(selectionManager, staffMeasusureFactory, scoreLayoutDictionary);
            var staffSystemFactory = new VisualStaffSystemFactory(systemMeasureFactory, selectionManager, scoreLayoutDictionary);
            var sceneFactory = new PageViewSceneFactory(staffSystemFactory, pageSize, 20, 30, ColorARGB.Black, ColorARGB.White, scoreLayoutDictionary);
            var origin = new VisualScoreDocumentScene(sceneFactory, score);
            var sceneManager = new SceneManager<IUniqueScoreElement, int>(origin, e => e.Id)
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
