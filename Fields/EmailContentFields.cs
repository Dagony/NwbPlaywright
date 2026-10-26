using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class EmailContentFields
    {
        public string? Aanhef { get; set; }

        public string? Geldnemer { get; set; }

        public string? Hoofdsom { get; set; }

        public string? Ingangsdatum { get; set; }

        public string? Einddatum { get; set; }

        public string? Renteherziening { get; set; }

        public string? Notional { get; set; } = "125.423";

        public string? Coupondag { get; set; } = "25";

        public string? EersteCoupondatum { get; set; } = "maandag 03 juli 2023";

        public string? LaatsteCoupondatum { get; set; } = "vrijdag 07 juli 2023";

        public string? Aflossing { get; set; } = "Lineair";

        public string? AantalAflossingen { get; set; } = "33";

        public string? AflossingsFreq { get; set; } = "Per kwartaal";

        public string? Rente { get; set; } = "2,250";

        public string? RenteConventie { get; set; } = "Ebond";

        public string? RenteFrequentie { get; set; } = "Jaarlijks";

        public string? Euribor { get; set; }

        public string? KredietOpslagEuribor { get; set; }

        public string? FinalSpreadEuribor { get; set; }

        public string? ZekerHeid { get; set; } = "Stichting Waarborgfonds";

        public string? OpMerkingen { get; set; } = "DealRemark2";

        // WSW Sentences && Basic Sentences for offer email FIXED
        public string? FootTextBasicSentenceOne { get; } = "Dit tarief is 5 minuten handelbaar. Wij horen graag of u hier gebruik van wilt maken.";

        public string? FootTextBasicSentenceTwo { get; } = "Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door uw gevraagde bedrag dient te worden overgemaakt. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een geldleningsovereenkomst die door onze back office aan u zal worden toegestuurd.";

        public string? FootTextBasicSentenceThree { get; } = "Op deze lening zullen de Algemene Voorwaarden NWB Bank van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden";

        public string? FootTextBasicSentenceFour { get; } = "De NWB Bank kan pas tot storting van de lening overgaan nadat de geldleningsovereenkomst door alle partijen rechtsgeldig is ondertekend en door de NWB Bank is ontvangen. Mocht onverhoopt de ondertekening van de geldleningsovereenkomst niet tijdig rondkomen, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door de NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door de NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.";

        // ---------------------------------------------------------------------------------------------------------------------------------------\\
        public string? FootTextWSWSentenceOne { get; } = "Dit tarief is 5 minuten handelbaar. Wij horen graag of u hiervan gebruik wilt maken.";

        public string? FootTextWSWSentenceTwo { get; } = "Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WSW standaard geldleningsovereenkomst die door onze back office aan u zal worden toegestuurd.";

        public string? FootTextWSWSentenceThree { get; } = "Op deze lening zullen de “Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021” van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie";

        public string? FootTextWSWSentenceFour { get; } = "De NWB Bank kan pas tot storting van de lening overgaan nadat de geldleningsovereenkomst inclusief garantie door alle partijen rechtsgeldig is ondertekend en door de NWB Bank is ontvangen. Mocht onverhoopt de ondertekening van de geldleningsovereenkomst inclusief de garantie niet tijdig rondkomen, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door de NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door de NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.";

        // Bodytext always has the same two final sentences for offer email FIXED
        public string? FootTextBasicFinalSentenceOne { get; } = "Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.";

        public string? FootTextBasicFinalSentenceTwo { get; } = "Bovenstaande is onder voorbehoud van typefouten.";

        public string? EntireFootBodyBasicLoan { get; } = @"Dit tarief is 5 minuten handelbaar. Wij horen graag of u hier gebruik van wilt maken.

Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door uw gevraagde bedrag dient te worden overgemaakt. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een geldleningsovereenkomst die door onze back office aan u zal worden toegestuurd.

Op deze lening zullen de Algemene Voorwaarden NWB Bank van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden
De NWB Bank kan pas tot storting van de lening overgaan nadat de geldleningsovereenkomst door alle partijen rechtsgeldig is ondertekend en door de NWB Bank is ontvangen. Mocht onverhoopt de ondertekening van de geldleningsovereenkomst niet tijdig rondkomen, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door de NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door de NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht. 

Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen. 

Bovenstaande is onder voorbehoud van typefouten.";

        public string? EntireFootBodyWSW { get; } = @"Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WSW standaard geldleningsovereenkomst die door onze back office aan u zal worden toegestuurd.


Op deze lening zullen de 'Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021' van toepassing zijn.Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie


De NWB Bank kan pas tot storting van de lening overgaan nadat de geldleningsovereenkomst inclusief garantie door alle partijen rechtsgeldig is ondertekend en door de NWB Bank is ontvangen.Mocht onverhoopt de ondertekening van de geldleningsovereenkomst inclusief de garantie niet tijdig rondkomen, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen.In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door de NWB Bank besloten wordt om de lening door te halen.Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door de NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.

Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven.U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen(juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.
";
    }
}