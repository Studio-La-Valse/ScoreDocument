using StudioLaValse.ScoreDocument.Models.Base;
using System;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class GraceChordLayout : IChordLayout
    {
        private readonly IGraceGroupLayout graceGroupLayout;

        public double XOffset => 0;

        public double SpaceRight => graceGroupLayout.ChordSpacing;

        public GraceChordLayout(IGraceGroupLayout graceGroupLayout)
        {
            this.graceGroupLayout = graceGroupLayout;
        }

        public void ApplyMemento(GraceChordLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }
        }
        public void ApplyMemento(GraceChordLayoutModel? memento)
        {
            ApplyMemento(memento as GraceChordLayoutMembers);
        }

        public void Restore()
        {

        }

    }

    public class AuthorGraceChordLayout : GraceChordLayout, ILayout<GraceChordLayoutMembers>
    {
        public AuthorGraceChordLayout(IGraceGroupLayout graceGroupLayout) : base(graceGroupLayout) 
        {
            
        }

        public GraceChordLayoutMembers GetMemento()
        {
            return new GraceChordLayoutMembers()
            {

            };
        }
    }

    public class UserGraceChordLayout : GraceChordLayout
    {
        private readonly Guid guid;

        public UserGraceChordLayout(Guid guid, IGraceGroupLayout graceGroupLayout) : base(graceGroupLayout)
        {
            this.guid = guid;
        }


        public GraceChordLayoutModel GetMemento()
        {
            return new GraceChordLayoutModel()
            {
                Id = guid
            };
        }
    }
}
