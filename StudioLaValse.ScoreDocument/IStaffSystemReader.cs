﻿using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStaffSystemReader : IStaffSystem<IStaffGroupReader, IScoreMeasureReader>, IScoreElementReader
    {

    }
}
