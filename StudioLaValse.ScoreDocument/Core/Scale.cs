namespace StudioLaValse.ScoreDocument.Core
{
    public class Scale
    {
        private readonly Step origin;
        private readonly ScaleStructure scaleStructure;



        public Scale(Step origin, ScaleStructure scaleStructure)
        {
            this.origin = origin;
            this.scaleStructure = scaleStructure;
        }


        public IEnumerable<Step> EnumerateSteps()
        {
            var number = scaleStructure.Length + 1;
            return EnumerateSteps(number);
        }

        public IEnumerable<Step> EnumerateSteps(int number)
        {
            if(number <= 0)
            {
                yield break;
            }

            var step = origin;
            yield return new Step(step.StepsFromC, step.Shifts);

            for (int i = 0; i < number - 1; i++)
            {
                var interval = scaleStructure.Intervals.ElementAt(i % scaleStructure.Length);
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
            var newStep = step.StepsFromC % 7;
            var newShifts = step.Shifts;
            for (int _ = 0; _ < interval.Steps; _++)
            {
                var newStepIsBorE = newStep == 2 || newStep == 6;
                if (newStepIsBorE)
                {
                    newShifts++;
                }

                newStep++;
            }

            newShifts += interval.Shifts;
            return new Step(newStep, newShifts);
        }


        public IEnumerable<Pitch> EnumeratePitches(Pitch pitch, int number)
        {
            if (number <= 0)
            {
                yield break;
            }

            var stepsInScale = EnumerateSteps().ToArray();
            var indexOfPitchInScale = stepsInScale.IndexOf(i => i.Equals(pitch.Step));
            if (indexOfPitchInScale == -1)
            {
                throw new Exception("Cannot find pitch in this scale. The pitch has to be part of the scale to start enumeration.");
            }

            yield return pitch;
            var octave = pitch.Octave;
            for (int i = indexOfPitchInScale; i < indexOfPitchInScale + (number - 1); i++)
            {
                var safeIndexOfCurrentStep = MathUtils.UnsignedModulo(i, scaleStructure.Length);
                var safeIndexOfNextStep = MathUtils.UnsignedModulo(i + 1, scaleStructure.Length);

                var currentStep = stepsInScale[safeIndexOfCurrentStep];
                var nextStep = stepsInScale[safeIndexOfNextStep];
                var intervalToNext = scaleStructure.Intervals.ElementAt(safeIndexOfCurrentStep);
                if (intervalToNext.SemiTones + currentStep.SemiTones >= 12)
                {
                    octave++;
                }

                pitch = new Pitch(nextStep, octave);
                yield return pitch;
            }
        }
        public IEnumerable<Pitch> EnumeratePitchesDownwards(Pitch pitch, int number)
        {
            if (number <= 0)
            {
                yield break;
            }

            var stepsInScale = EnumerateSteps().ToArray();
            var indexOfPitchInScale = stepsInScale.IndexOf(i => i.Equals(pitch.Step));
            if (indexOfPitchInScale == -1)
            {
                throw new Exception("Cannot find pitch in this scale. The pitch has to be part of the scale to start enumeration.");
            }

            yield return pitch;
            var octave = pitch.Octave;
            for (int i = indexOfPitchInScale; i > indexOfPitchInScale - (number - 1); i--)
            {
                var safeIndexOfCurrentStep = MathUtils.UnsignedModulo(i, scaleStructure.Length);
                var safeIndexOfNextStep = MathUtils.UnsignedModulo(i - 1, scaleStructure.Length);

                var currentStep = stepsInScale[safeIndexOfCurrentStep];
                var nextStep = stepsInScale[safeIndexOfNextStep];
                var intervalToNextStep = scaleStructure.Intervals.ElementAt(safeIndexOfCurrentStep);
                if (currentStep.SemiTones - intervalToNextStep.SemiTones <= 0)
                {
                    octave--;
                }

                pitch = new Pitch(nextStep, octave);
                yield return pitch;
            }
        }

        public bool Contains(Step step)
        {
            return EnumerateSteps().Any(_step => _step.Equals(step));
        }
    }
}
