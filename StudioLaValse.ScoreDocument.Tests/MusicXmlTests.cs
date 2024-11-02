using StudioLaValse.Drawable;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Models.V1;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;
using StudioLaValse.ScoreDocument.MusicXml;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.Tests;

[TestClass]
public class MusicXmlTests
{
    [TestMethod]
    public void TestChords()
    {
        var xDocument = XDocument.Parse(input);
        var scoreDocumentModel = new ScoreDocumentModel() { Id = Guid.NewGuid(), InstrumentRibbons = [], ScoreMeasures = [] };

        var positionComparer = new PositionComparer();
        var postitionDictionaryBuilder = new PositionDictionaryBuilder(positionComparer);
        var styleTemplate = ScoreDocumentStyleTemplate.Create();
        var scoreDocument = Implementation.ScoreDocument.Create(styleTemplate, scoreDocumentModel, postitionDictionaryBuilder);
        scoreDocument.BuildFromXml(xDocument);
    }

    const string input = """
        <?xml version="1.0" encoding="UTF-8" standalone="no"?>
        <!DOCTYPE score-partwise PUBLIC "-//Recordare//DTD MusicXML 3.1 Partwise//EN" "http://www.musicxml.org/dtds/partwise.dtd">
        <score-partwise version="3.1">
          <movement-title>Sheep May Safely Graze</movement-title>
          <identification>
            <creator type="composer">Bach</creator>
            <creator type="arranger">Roel Westrik</creator>
            <rights>© 2023</rights>
            <encoding>
              <software>Finale v25 for Windows</software>
              <encoding-date>2024-08-11</encoding-date>
              <supports attribute="new-system" element="print" type="yes" value="yes"/>
              <supports attribute="new-page" element="print" type="yes" value="yes"/>
              <supports element="accidental" type="yes"/>
              <supports element="beam" type="yes"/>
              <supports element="stem" type="yes"/>
            </encoding>
          </identification>
          <defaults>
            <scaling>
              <millimeters>7.2319</millimeters>
              <tenths>40</tenths>
            </scaling>
            <page-layout>
              <page-height>1545</page-height>
              <page-width>1194</page-width>
              <page-margins type="both">
                <left-margin>140</left-margin>
                <right-margin>70</right-margin>
                <top-margin>70</top-margin>
                <bottom-margin>70</bottom-margin>
              </page-margins>
            </page-layout>
            <system-layout>
              <system-margins>
                <left-margin>0</left-margin>
                <right-margin>0</right-margin>
              </system-margins>
              <system-distance>121</system-distance>
              <top-system-distance>70</top-system-distance>
            </system-layout>
            <staff-layout>
              <staff-distance>80</staff-distance>
            </staff-layout>
            <appearance>
              <line-width type="stem">0.7487</line-width>
              <line-width type="beam">5</line-width>
              <line-width type="staff">0.7487</line-width>
              <line-width type="light barline">0.7487</line-width>
              <line-width type="heavy barline">5</line-width>
              <line-width type="leger">0.7487</line-width>
              <line-width type="ending">0.7487</line-width>
              <line-width type="wedge">0.7487</line-width>
              <line-width type="enclosure">0.7487</line-width>
              <line-width type="tuplet bracket">0.7487</line-width>
              <note-size type="grace">60</note-size>
              <note-size type="cue">60</note-size>
              <distance type="hyphen">120</distance>
              <distance type="beam">8</distance>
            </appearance>
            <music-font font-family="Maestro,engraved" font-size="20.5"/>
            <word-font font-family="Times New Roman" font-size="10.25"/>
          </defaults>
          <credit page="1">
            <credit-type>title</credit-type>
            <credit-words default-x="632" default-y="1475" font-size="24" justify="center" valign="top">Sheep May Safely Graze</credit-words>
          </credit>
          <credit page="1">
            <credit-type>composer</credit-type>
            <credit-words default-x="1122" default-y="1407" font-size="12" justify="right" valign="top">Bach</credit-words>
          </credit>
          <credit page="1">
            <credit-type>rights</credit-type>
            <credit-words default-x="632" default-y="53" font-size="10" justify="center" valign="bottom">© 2023</credit-words>
          </credit>
          <credit page="1">
            <credit-words default-x="140" default-y="1478" font-size="12" valign="top">Score</credit-words>
          </credit>
          <credit page="1">
            <credit-type>arranger</credit-type>
            <credit-words default-x="1122" default-y="1372" font-size="12" justify="right" valign="top">Roel Westrik</credit-words>
          </credit>
          <credit page="2">
            <credit-type>page number</credit-type>
            <credit-words default-x="140" default-y="1497" font-size="12" valign="top">2</credit-words>
          </credit>
          <credit page="2">
            <credit-type>title</credit-type>
            <credit-words default-x="632" default-y="1499" font-size="12" justify="center" valign="top">Sheep May Safely Graze</credit-words>
          </credit>
          <credit page="3">
            <credit-type>page number</credit-type>
            <credit-words default-x="1124" default-y="1499" font-size="12" halign="right" valign="top">3</credit-words>
          </credit>
          <credit page="3">
            <credit-type>title</credit-type>
            <credit-words default-x="632" default-y="1499" font-size="12" justify="center" valign="top">Sheep May Safely Graze</credit-words>
          </credit>
          <credit page="4">
            <credit-type>page number</credit-type>
            <credit-words default-x="140" default-y="1497" font-size="12" valign="top">4</credit-words>
          </credit>
          <credit page="4">
            <credit-type>title</credit-type>
            <credit-words default-x="632" default-y="1499" font-size="12" justify="center" valign="top">Sheep May Safely Graze</credit-words>
          </credit>
          <credit page="5">
            <credit-type>page number</credit-type>
            <credit-words default-x="1124" default-y="1499" font-size="12" halign="right" valign="top">5</credit-words>
          </credit>
          <credit page="5">
            <credit-type>title</credit-type>
            <credit-words default-x="632" default-y="1499" font-size="12" justify="center" valign="top">Sheep May Safely Graze</credit-words>
          </credit>
          <part-list>
            <score-part id="P1">
              <part-name print-object="no">MusicXML Part</part-name>
              <score-instrument id="P1-I1">
                <instrument-name>SmartMusic SoftSynth</instrument-name>
                <virtual-instrument/>
              </score-instrument>
              <midi-device>SmartMusic SoftSynth</midi-device>
              <midi-instrument id="P1-I1">
                <midi-channel>1</midi-channel>
                <midi-bank>15489</midi-bank>
                <midi-program>1</midi-program>
                <volume>80</volume>
                <pan>0</pan>
              </midi-instrument>
            </score-part>
          </part-list>
          <!--=========================================================-->
          <part id="P1">
            <measure number="1" width="488">
              <print page-number="1">
                <system-layout>
                  <system-margins>
                    <left-margin>70</left-margin>
                    <right-margin>0</right-margin>
                  </system-margins>
                  <top-system-distance>206</top-system-distance>
                </system-layout>
                <measure-numbering>system</measure-numbering>
              </print>
              <attributes>
                <divisions>8</divisions>
                <key>
                  <fifths>-2</fifths>
                  <mode>major</mode>
                </key>
                <time symbol="common">
                  <beats>4</beats>
                  <beat-type>4</beat-type>
                </time>
                <staves>2</staves>
                <clef number="1">
                  <sign>G</sign>
                  <line>2</line>
                </clef>
                <clef number="2">
                  <sign>F</sign>
                  <line>4</line>
                </clef>
              </attributes>
              <sound tempo="50"/>
              <direction directive="yes" placement="above">
                <direction-type>
                  <words default-y="50" font-size="12" font-weight="bold">Andante piacevole</words>
                </direction-type>
                <staff>1</staff>
              </direction>
              <direction placement="below">
                <direction-type>
                  <words default-y="-88" font-style="italic" relative-x="-4">simplice, tranquilo</words>
                </direction-type>
                <staff>1</staff>
              </direction>
              <note default-x="123">
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem default-y="35">up</stem>
                <staff>1</staff>
                <beam number="1">begin</beam>
              </note>
              <note default-x="123">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="166">
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem default-y="32">up</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
                <beam number="2">begin</beam>
                <notations>
                  <slur number="1" placement="above" type="start"/>
                </notations>
              </note>
              <note default-x="166">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>5</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="193">
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem default-y="30">up</stem>
                <staff>1</staff>
                <beam number="1">end</beam>
                <beam number="2">end</beam>
                <notations>
                  <slur number="1" type="stop"/>
                </notations>
              </note>
              <note default-x="193">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="220">
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem default-y="35">up</stem>
                <staff>1</staff>
                <beam number="1">begin</beam>
              </note>
              <note default-x="220">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="262">
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem default-y="32">up</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
                <beam number="2">begin</beam>
                <notations>
                  <slur number="1" placement="above" type="start"/>
                </notations>
              </note>
              <note default-x="262">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>5</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="289">
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem default-y="30">up</stem>
                <staff>1</staff>
                <beam number="1">end</beam>
                <beam number="2">end</beam>
                <notations>
                  <slur number="1" type="stop"/>
                </notations>
              </note>
              <note default-x="289">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>2</duration>
                <voice>1</voice>
                <type>16th</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="315">
                <pitch>
                  <step>D</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem default-y="45">up</stem>
                <staff>1</staff>
                <beam number="1">begin</beam>
                <notations>
                  <slur number="1" placement="above" type="start"/>
                </notations>
              </note>
              <note default-x="315">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="359">
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem default-y="46.5">up</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
                <notations>
                  <slur number="1" type="stop"/>
                </notations>
              </note>
              <note default-x="359">
                <chord/>
                <pitch>
                  <step>G</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="401">
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem default-y="48.5">up</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
                <notations>
                  <slur number="1" placement="above" type="start"/>
                </notations>
              </note>
              <note default-x="401">
                <chord/>
                <pitch>
                  <step>G</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <note default-x="445">
                <pitch>
                  <step>G</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem default-y="50">up</stem>
                <staff>1</staff>
                <beam number="1">end</beam>
                <notations>
                  <slur number="1" type="stop"/>
                </notations>
              </note>
              <note default-x="445">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>1</voice>
                <type>eighth</type>
                <stem>up</stem>
                <staff>1</staff>
              </note>
              <backup>
                <duration>32</duration>
              </backup>
              <note default-x="123">
                <pitch>
                  <step>F</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-70">down</stem>
                <staff>1</staff>
                <beam number="1">begin</beam>
              </note>
              <note default-x="123">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="166">
                <pitch>
                  <step>F</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-70">down</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
              </note>
              <note default-x="166">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="220">
                <pitch>
                  <step>F</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-70">down</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
              </note>
              <note default-x="220">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="262">
                <pitch>
                  <step>F</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-70">down</stem>
                <staff>1</staff>
                <beam number="1">end</beam>
              </note>
              <note default-x="262">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="315">
                <pitch>
                  <step>G</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-65">down</stem>
                <staff>1</staff>
                <beam number="1">begin</beam>
              </note>
              <note default-x="315">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="359">
                <pitch>
                  <step>G</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-63">down</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
              </note>
              <note default-x="359">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="401">
                <pitch>
                  <step>G</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-62">down</stem>
                <staff>1</staff>
                <beam number="1">continue</beam>
              </note>
              <note default-x="389">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="401">
                <chord/>
                <pitch>
                  <step>C</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="432">
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem default-y="-60">down</stem>
                <staff>1</staff>
                <beam number="1">end</beam>
              </note>
              <note default-x="445">
                <chord/>
                <pitch>
                  <step>C</step>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <note default-x="445">
                <chord/>
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>5</octave>
                </pitch>
                <duration>4</duration>
                <voice>2</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>1</staff>
              </note>
              <backup>
                <duration>32</duration>
              </backup>
              <note default-x="123">
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>1</octave>
                </pitch>
                <duration>32</duration>
                <voice>3</voice>
                <type>whole</type>
                <staff>2</staff>
              </note>
              <note default-x="123">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>2</octave>
                </pitch>
                <duration>32</duration>
                <voice>3</voice>
                <type>whole</type>
                <staff>2</staff>
              </note>
              <backup>
                <duration>32</duration>
              </backup>
              <note default-x="123">
                <rest>
                  <display-step>C</display-step>
                  <display-octave>4</display-octave>
                </rest>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <staff>2</staff>
              </note>
              <note default-x="166">
                <pitch>
                  <step>D</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-55">down</stem>
                <staff>2</staff>
                <beam number="1">begin</beam>
                <notations>
                  <slur number="1" placement="below" type="start"/>
                  <articulations>
                    <detached-legato default-x="1" default-y="-70" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="166">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="166">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="166">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="220">
                <pitch>
                  <step>D</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-55">down</stem>
                <staff>2</staff>
                <beam number="1">continue</beam>
                <notations>
                  <articulations>
                    <detached-legato default-x="0" default-y="-70" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="220">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="220">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="220">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="262">
                <pitch>
                  <step>D</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-55">down</stem>
                <staff>2</staff>
                <beam number="1">end</beam>
                <notations>
                  <articulations>
                    <detached-legato default-x="1" default-y="-70" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="262">
                <chord/>
                <pitch>
                  <step>F</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="262">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="262">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="315">
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-60">down</stem>
                <staff>2</staff>
                <beam number="1">begin</beam>
                <notations>
                  <articulations>
                    <detached-legato default-x="1" default-y="-75" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="315">
                <chord/>
                <pitch>
                  <step>G</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="315">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="315">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="359">
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-60">down</stem>
                <staff>2</staff>
                <beam number="1">continue</beam>
                <notations>
                  <articulations>
                    <detached-legato default-x="1" default-y="-75" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="359">
                <chord/>
                <pitch>
                  <step>G</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="359">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="359">
                <chord/>
                <pitch>
                  <step>D</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="401">
                <pitch>
                  <step>C</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-60">down</stem>
                <staff>2</staff>
                <beam number="1">continue</beam>
                <notations>
                  <articulations>
                    <detached-legato default-x="1" default-y="-75" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="401">
                <chord/>
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="401">
                <chord/>
                <pitch>
                  <step>G</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="389">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="401">
                <chord/>
                <pitch>
                  <step>C</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="445">
                <pitch>
                  <step>C</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem default-y="-60">down</stem>
                <staff>2</staff>
                <beam number="1">end</beam>
                <notations>
                  <slur number="1" type="stop"/>
                  <articulations>
                    <detached-legato default-x="1" default-y="-75" placement="below"/>
                  </articulations>
                </notations>
              </note>
              <note default-x="445">
                <chord/>
                <pitch>
                  <step>E</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="445">
                <chord/>
                <pitch>
                  <step>G</step>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="432">
                <chord/>
                <pitch>
                  <step>B</step>
                  <alter>-1</alter>
                  <octave>3</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
              <note default-x="445">
                <chord/>
                <pitch>
                  <step>C</step>
                  <octave>4</octave>
                </pitch>
                <duration>4</duration>
                <voice>4</voice>
                <type>eighth</type>
                <stem>down</stem>
                <staff>2</staff>
              </note>
            </measure>
          </part>
        </score-partwise>
        
        """;
}
