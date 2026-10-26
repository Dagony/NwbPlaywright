using NUnit.Framework;

namespace TA.Playwright.Features.Long
{
    // TC03/TC04 create a case against the SUT that can lock when another test hits it at the
    // same time, so this fixture must not run concurrently with other fixtures/workers.
    // Kept in a separate partial file so it survives Reqnroll regenerating CsvFilesFixed.feature.cs.
    [NonParallelizable]
    public partial class CsvFilesFixedFeature
    {
    }
}