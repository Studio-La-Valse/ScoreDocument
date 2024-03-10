namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical scale.
    /// </summary>
    public class Scale
    {
        private readonly Step origin;
        private readonly ScaleStructure scaleStructure;


        /// <summary>
        /// Construct a scale from an origin and a scale structure.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="scaleStructure"></param>
        public Scale(Step origin, ScaleStructure scaleStructure)
        {
            this.origin = origin;
            this.scaleStructure = scaleStructure;
        }

        /// <summary>
        /// Enumerate the steps in the scale.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Step> EnumerateSteps()
        {
            int number = scaleStructure.Length + 1;
            return EnumerateSteps(number);
        }

        /// <summary>
        /// Enumerate the steps in this scale for a number of steps.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<Step> EnumerateSteps(int number)
        {
            if (number <= 0)
            {
                yield break;
            }

            Step step = origin;
            yield return new Step(step.StepsFromC, step.Shifts);

            for (int i = 0; i < number - 1; i++)
            {
                Interval interval = scaleStructure.Intervals.ElementAt(i % scaleStructure.Length);
                step = IncrementInterval(step, interval);
                yield return step;
            }
        }

        /// <summary>
        /// THIS METHOD IS PRIVATE BECAUSE IT DOES NOT WORK WITH INTERVALS GREATER THAN ONE STEP
        /// </summary>
        /// <param name="step"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        private static Step IncrementInterval(Step step, Interval interval)
        {
            int newStep = step.StepsFromC % 7;
            int newShifts = step.Shifts;
            for (int _ = 0; _ < interval.Steps; _++)
            {
                bool newStepIsBorE = newStep is 2 or 6;
                if (newStepIsBorE)
                {
                    newShifts++;
                }

                newStep++;
            }

            newShifts += interval.Shifts;
            return new Step(newStep, newShifts);
        }

        /// <summary>
        /// Specifies whether the scale contains the specified step.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public bool Contains(Step step)
        {
            return EnumerateSteps().Any(_step => _step.Equals(step));
        }
    }
}
