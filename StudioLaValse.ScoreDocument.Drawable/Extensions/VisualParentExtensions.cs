namespace StudioLaValse.ScoreDocument.Drawable.Extensions
{
    /// <summary>
    /// Extensions to visual elements.
    /// </summary>
    public static class VisualParentExtensions
    {
        /// <summary>
        /// Specify the target factory to use a selection.
        /// </summary>
        /// <param name="visualInstrumentMeasureFactory"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static IVisualInstrumentMeasureFactory UseSelection(this IVisualInstrumentMeasureFactory visualInstrumentMeasureFactory, ISelection<IUniqueScoreElement> selection)
        {
            return new VisualInstrumentMeasureFactoryWithSelection(visualInstrumentMeasureFactory, selection);
        }

        /// <summary>
        /// Specify the target factory to use a selection.
        /// </summary>
        /// <param name="visualNoteFactory"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static IVisualNoteFactory UseSelection(this IVisualNoteFactory visualNoteFactory, ISelection<IUniqueScoreElement> selection)
        {
            return new VisualNoteFactoryWithSelection(visualNoteFactory, selection);
        }

        /// <summary>
        /// Specify the target factory to use a selection.
        /// </summary>
        /// <param name="visualRestFactory"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static IVisualRestFactory UseSelection(this IVisualRestFactory visualRestFactory, ISelection<IUniqueScoreElement> selection)
        {
            return new VisualRestFactoryWithSelection(visualRestFactory, selection);
        }

        /// <summary>
        /// Specify the target factory to use a selection.
        /// </summary>
        /// <param name="visualSystemMeasureFactory"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static IVisualSystemMeasureFactory UseSelection(this IVisualSystemMeasureFactory visualSystemMeasureFactory, ISelection<IUniqueScoreElement> selection)
        {
            return new VisualSystemMeasureFactoryWithSelection(visualSystemMeasureFactory, selection);
        }

        /// <summary>
        /// Specify the target factory to use a selection.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="visualParent"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static BaseContentWrapper UseSelection<TEntity>(this BaseVisualParent<TEntity> visualParent, ISelection<TEntity> selection) where TEntity : class
        {
            var wrapperWithSelection = new BaseContentWrapperWithSelection<TEntity>(visualParent, visualParent.AssociatedElement, selection);
            return wrapperWithSelection;
        }

        /// <summary>
        /// Ensure the specified <see cref="BaseContentWrapper"/> is becomes a <see cref="BaseVisualParent{TEntity}"/>. 
        /// If the element is already a <see cref="BaseVisualParent{TEntity}"/> and its <see cref="BaseVisualParent{TEntity}.AssociatedElement"/> does not equal the specified parent, 
        /// an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="contentWrapper"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static BaseVisualParent<TEntity> EnsureVisualParent<TEntity>(this BaseContentWrapper contentWrapper, TEntity parent) where TEntity : class
        {
            if (contentWrapper is BaseVisualParent<TEntity> _parent)
            {
                if (!_parent.AssociatedElement.Equals(parent))
                {
                    throw new InvalidOperationException("Content wrapper is already a visual parent, but it's associated element does not match the specified target parent.");
                }

                return _parent;
            }

            var visualParent = contentWrapper.ToVisualParent(parent);
            return visualParent;
        }

        /// <summary>
        /// Upgrades a <see cref="BaseContentWrapper"/> to become a <see cref="BaseVisualParent{TEntity}"/> by attaching the specified entity.
        /// If the <see cref="BaseContentWrapper"/> is already a <see cref="BaseVisualParent{TEntity}"/>, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="contentWrapper"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static BaseVisualParent<TEntity> ToVisualParent<TEntity>(this BaseContentWrapper contentWrapper, TEntity entity) where TEntity : class
        {
            if (contentWrapper is BaseVisualParent<TEntity>)
            {
                throw new InvalidOperationException("Content wrapper is already a visual parent.");
            }

            var visualParent = new VisualParentFromContentWrapper<TEntity>(entity, contentWrapper);
            return visualParent;
        }

        class VisualInstrumentMeasureFactoryWithSelection : IVisualInstrumentMeasureFactory
        {
            private readonly IVisualInstrumentMeasureFactory source;
            private readonly ISelection<IUniqueScoreElement> selection;

            public VisualInstrumentMeasureFactoryWithSelection(IVisualInstrumentMeasureFactory source, ISelection<IUniqueScoreElement> selection)
            {
                this.source = source;
                this.selection = selection;
            }
            public BaseContentWrapper CreateContent(IInstrumentMeasure source, IStaffGroup staffGroup, IReadOnlyDictionary<Position, double> positionDictionary, double canvasTop, double canvasLeft, double width)
            {
                var baseContentWrapper = this.source
                    .CreateContent(source, staffGroup, positionDictionary, canvasTop, canvasLeft, width)
                    .EnsureVisualParent<IUniqueScoreElement>(source)
                    .UseSelection(selection);
                return baseContentWrapper;
            }
        }

        class VisualNoteFactoryWithSelection : IVisualNoteFactory
        {
            private readonly IVisualNoteFactory source;
            private readonly ISelection<IUniqueScoreElement> selection;

            public VisualNoteFactoryWithSelection(IVisualNoteFactory source, ISelection<IUniqueScoreElement> selection)
            {
                this.source = source;
                this.selection = selection;
            }
            public BaseContentWrapper CreateContent(INote note, Clef clef, Accidental? accidental, double canvasLeft, double canvasTop)
            {
                var baseContentWrapper = source
                    .CreateContent(note, clef, accidental, canvasLeft, canvasTop)
                    .EnsureVisualParent<IUniqueScoreElement>(note)
                    .UseSelection(selection);
                return baseContentWrapper;
            }
        }

        class VisualRestFactoryWithSelection : IVisualRestFactory
        {
            private readonly IVisualRestFactory source;
            private readonly ISelection<IUniqueScoreElement> selection;

            public VisualRestFactoryWithSelection(IVisualRestFactory source, ISelection<IUniqueScoreElement> selection)
            {
                this.source = source;
                this.selection = selection;
            }
            public BaseContentWrapper CreateContent(IChord element, double canvasLeft, double canvasTop)
            {
                var baseContentWrapper = source
                    .CreateContent(element, canvasLeft, canvasTop)
                    .EnsureVisualParent<IUniqueScoreElement>(element)
                    .UseSelection(selection);
                return baseContentWrapper;
            }
        }

        class VisualSystemMeasureFactoryWithSelection : IVisualSystemMeasureFactory
        {
            private readonly IVisualSystemMeasureFactory source;
            private readonly ISelection<IUniqueScoreElement> selection;

            public VisualSystemMeasureFactoryWithSelection(IVisualSystemMeasureFactory source, ISelection<IUniqueScoreElement> selection)
            {
                this.source = source;
                this.selection = selection;
            }
            public BaseContentWrapper CreateContent(IScoreMeasure scoreMeasure, IStaffSystem staffSystem, double canvasLeft, double canvasTop, double width)
            {
                var baseContentWrapper = source
                    .CreateContent(scoreMeasure, staffSystem, canvasLeft, canvasTop, width)
                    .EnsureVisualParent<IUniqueScoreElement>(scoreMeasure)
                    .UseSelection(selection);
                return baseContentWrapper;
            }
        }

        class BaseContentWrapperWithSelection<TEntity> : BaseSelectableParent<TEntity> where TEntity : class
        {
            private readonly BaseContentWrapper baseContentWrapper;

            public BaseContentWrapperWithSelection(BaseContentWrapper baseContentWrapper, TEntity entity, ISelection<TEntity> selection) : base(entity, selection)
            {
                this.baseContentWrapper = baseContentWrapper;
            }

            public override IEnumerable<BaseDrawableElement> GetDrawableElements()
            {
                return baseContentWrapper.GetDrawableElements();
            }

            public override IEnumerable<BaseContentWrapper> GetContentWrappers()
            {
                var wrappers = baseContentWrapper.GetContentWrappers();
                foreach (var wrapper in wrappers)
                {
                    yield return wrapper;
                }

                var ghost = new SimpleGhost<TEntity>(this);
                yield return ghost;
            }
            public override bool Respond(XY point)
            {
                return BoundingBox().Contains(point);
            }
            public override bool OnMouseMove(XY mousePosition)
            {
                var currentlyMouseOver = IsMouseOver;
                IsMouseOver = BoundingBox().Contains(mousePosition);
                return currentlyMouseOver != IsMouseOver;
            }
        }

        class VisualParentFromContentWrapper<TEntity> : BaseVisualParent<TEntity> where TEntity : class
        {
            private readonly BaseContentWrapper contentWrapper;

            public VisualParentFromContentWrapper(TEntity entity, BaseContentWrapper contentWrapper) : base(entity)
            {
                this.contentWrapper = contentWrapper;
            }

            public override IEnumerable<BaseContentWrapper> GetContentWrappers()
            {
                return contentWrapper.GetContentWrappers();
            }

            public override IEnumerable<BaseDrawableElement> GetDrawableElements()
            {
                return contentWrapper.GetDrawableElements();
            }
        }
    }
}
