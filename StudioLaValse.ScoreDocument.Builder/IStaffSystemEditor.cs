﻿using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <inheritdoc/>
    public interface IStaffSystemEditor : IStaffSystem<IScoreMeasureEditor, IStaffGroupEditor>, ILayoutEditor<StaffSystemLayout>, IScoreElementEditor
    {

    }
}
