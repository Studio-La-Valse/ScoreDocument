using StudioLaValse.Drawable.Text;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.MusicXml;
using System.Reflection;
using System.Xml.Linq;
using StudioLaValse.Drawable.HTML;
using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.Drawable.Extensions;
using StudioLaValse.Drawable.DrawableElements;
using System.Diagnostics;
using StudioLaValse.Drawable.HTML.Extensions;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Tests.Svg;

internal class Program
{
    static void Main(string[] args)
    {
        var canvasWidth = PageSize.A4.Width;
        var canvasHeight = PageSize.A4.Height;
        var htmlCanvas = new HTMLCanvas(canvasWidth, canvasHeight);
        var canvasPainter = new NewHTMLCanvasPainter(htmlCanvas);

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "StudioLaValse.ScoreDocument.Tests.Svg.Resources.Radiohead Fade Out.musicxml";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        var document = XDocument.Load(stream!);

        var styleTemplate = ScoreDocumentStyleTemplate.Create();
        styleTemplate.PageStyleTemplate.PageWidth = canvasWidth;
        styleTemplate.PageStyleTemplate.PageHeight = canvasHeight;
        var positionComparer = new PositionComparer();
        var positionDictionaryBuilder = new PositionDictionaryBuilder(positionComparer);
        var scoreDocument = Implementation.ScoreDocument.Create(styleTemplate, positionDictionaryBuilder).BuildFromXml(document);

        var glyphLibrary = new GenericGlyphLibrary(scoreDocument);
        var restFactory = new VisualRestScene(glyphLibrary);
        var noteFactory = new VisualNoteScene(glyphLibrary);
        var noteGroupFactory = new VisualNoteGroupScene(noteFactory, restFactory, glyphLibrary);
        var instrumentMeasureFactory = new VisualInstrumentMeasureScene(noteGroupFactory, glyphLibrary);
        var systemMeasureFactory = new VisualSystemMeasureScene(instrumentMeasureFactory, positionDictionaryBuilder);
        var visualStaffFactory = new VisualStaffSystemScene(systemMeasureFactory, glyphLibrary, positionDictionaryBuilder);
        var visualPageFactory = new VisualPageScene(visualStaffFactory, positionDictionaryBuilder);
        var sceneFactory = new SinglePageViewScene(0, visualPageFactory, positionDictionaryBuilder);
        var scene = new VisualScoreDocumentScene(sceneFactory, scoreDocument);
        canvasPainter.DrawContentWrapper(scene);
        canvasPainter.FinishDrawing();

        var svgContent = 
            """
            <style>
                @font-face {
                    font-family: Bravura;
                    src: url('C:/Projects/Studio La Valse Repository/Sinfonia.API/Sinfonia.API/wwwroot/fonts/bravura/Bravura.otf');
                }
                svg{
                    font-family: Bravura;
                }
            </style>
            """ + htmlCanvas.SVGContent();

        var file = Path.Combine(Environment.CurrentDirectory, "index.html");
        File.WriteAllText(file, svgContent);

        using var fileopener = new Process();

        fileopener.StartInfo.FileName = "explorer";
        fileopener.StartInfo.Arguments = "\"" + file + "\"";
        fileopener.Start();
    }
}

internal class GenericGlyphLibrary : BaseGlyphLibrary
{
    private readonly IScoreDocumentLayout scoreDocumentLayout;
    private readonly double noteWidth = 1.75;
    public GenericGlyphLibrary(IScoreDocument scoreDocumentLayout)
    {
        this.scoreDocumentLayout = scoreDocumentLayout;
    }

    public override Uri FontFamilyKey => null!;

    public override string FontFamily => "Bravura";

    public override Glyph NoteHeadBlack(double scale)
    {
        var baseNote = base.NoteHeadBlack(scale);

        return new Glyph(
            baseNote.StringValue,
            baseNote.FontFamily,
            baseNote.Scale,
            noteWidth,
            null);
    }

    public override Glyph NoteHeadWhite(double scale)
    {
        var baseNote = base.NoteHeadWhite(scale);

        return new Glyph(
            baseNote.StringValue,
            baseNote.FontFamily,
            baseNote.Scale,
            noteWidth,
            null);
    }

    public override Glyph NoteHeadWhole(double scale)
    {
        var baseNote = base.NoteHeadWhole(scale);

        return new Glyph(
            baseNote.StringValue,
            baseNote.FontFamily,
            baseNote.Scale,
            noteWidth,
            null);
    }

    public override Glyph Sharp(double scale)
    {
        var baseNote = base.Sharp(scale);

        return new Glyph(
            baseNote.StringValue,
            baseNote.FontFamily,
            baseNote.Scale,
            noteWidth,
            null);
    }
}

public class NewHTMLCanvasPainter : HTMLCanvasPainter
{
    private readonly HTMLCanvas canvas;

    /// <inheritdoc/>
    public NewHTMLCanvasPainter(HTMLCanvas canvas) : base(canvas)
    {
        this.canvas = canvas;
    }


    /// <inheritdoc/>
    protected override void DrawElement(HTMLCanvas canvas, DrawableText text)
    {
        var x = $"{text.OriginX}".Replace(",", ".");

        // Formula below came from trial and error. I don't know why it works or why it is even needed but here we are.
        // The Bravura fonts somehow needs adjustment.. Its not necessary for other canvas types which makes it more confusing.
        var y = text.FontFamily.Name switch
        {
            "Bravura" => $"{text.OriginY - (text.FontSize * 0.14)}".Replace(",", "."),
            _ => $"{text.OriginY}".Replace(",", ".")
        };

        var fontStyle = 
            $"""
            style=fill:{text.Color.Svg()};" font-size="{text.FontSize.ToString().Replace(",", ".")}px" font-family="{text.FontFamily.Name}"
            """;

        var alignmentBase = text.VerticalAlignment switch
        {
            VerticalTextOrigin.Top => "hanging",
            VerticalTextOrigin.Center => "middle",
            VerticalTextOrigin.Bottom => "baseline",
            _ => throw new NotImplementedException(nameof(text.VerticalAlignment))
        };
        var textAnchor = text.HorizontalAlignment switch
        {
            HorizontalTextOrigin.Left => "start",
            HorizontalTextOrigin.Right => "end",
            HorizontalTextOrigin.Center => "middle",
            _ => throw new NotImplementedException(nameof(text.HorizontalAlignment))
        };

        var t =
            $"""
            <text alignment-baseline="{alignmentBase}" text-anchor="{textAnchor}" x="{x}" y="{y}" {fontStyle} visibility="visible">
                {text.Text}
            </text>
            """;

        canvas.Add(t);

    }
}
