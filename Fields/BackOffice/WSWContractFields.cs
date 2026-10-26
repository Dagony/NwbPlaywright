using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class WSWContractFields
    {
        public string? ResultPage1Counterparty { get; set; }

        public string? ResultAdress { get; set; }

        public string? KvkNumber { get; set; }

        public string? ResultMaturityDate { get; set; }

        public string? ResultArt1ResultNotionalInNumbers { get; set; }

        public string? ResultArt1ResultNotionalInWords { get; set; }

        public string? ResultArt4Rente { get; set; }

        public string? ResultArt4dot1CoupontFreq { get; set; }

        public string? ResultArt4dot2InterestRRdate { get; set; }

        public string? ResultArt1StartDatum { get; set; }

        public string? ResultLooptijd { get; set; }

        public string? ResultArt3MaturityDatum { get; set; }

        public string? ResultArt4ShowMultipleSections { get; set; }

        public string? Article4OnlyShowSection1 { get; } = @$"Artikel 4 De Geldgever verbindt zich jegens WSW met betrekking tot vorderingen voortvloeiend uit hoofde van of verband houdend met de met de Geldnemer gesloten Geldleningsovereenkomst geen of slechts een voorwaardelijke vordering in te zullen dienen in het faillissement of de surséance van betaling van de Geldnemer, en een reeds ingediende vordering in het faillissement of de surséance van betaling van de Geldnemer op eerste verzoek van WSW te zullen intrekken, zolang WSW zijn verplichtingen jegens de Geldgever uit hoofde van de Borgtocht nakomt. Indien een curator of een bewindvoerder daarom verzoekt, zal de Geldgever de niet-indiening of intrekking van die vordering(en) in het faillissement of de surséance van betaling van de Geldnemer zo nodig prompt schriftelijk aan WSW bevestigen.";

        public string? Article4ShowAllSections { get; } = @"2.	[Het in artikel 4 lid 1 bedoelde rentepercentage zal per niet van toepassing (de 'Wijzigingsdatum')[en voorts per XXXXX (de 'Wijzigingsdata')]  in onderling overleg tussen de Geldgever en de Geldnemer opnieuw worden vastgesteld.  Het voorstel tot rentevaststelling zal marktconform zijn voor dit type geldleningen en dient ten minste twee en ten hoogste drie maanden voor elke Wijzigingsdatum door de Geldgever c.q. de Geldnemer schriftelijk aan de wederpartij te worden gedaan. Indien een voorstel tot rentevaststelling niet binnen veertien dagen na dagtekening van het als eerste verzonden voorstel door de wederpartij wordt aanvaard, zal deze Geldleningsovereenkomst worden beëindigd en zal het restant van de hoofdsom met rente, kosten en eventueel verschuldigde vergoedingen voor het geheel à 100% worden afgelost.] 
        3.	[Het voorstel van de Geldgever tot rentevaststelling wordt geacht marktconform te zijn, indien de Geldnemer niet binnen bovenvermelde termijn van veertien dagen aantoont dat hij bij een andere financier een gelijksoortige geldlening als de onderhavige met dezelfde modaliteiten, doch tegen een lager effectief rendement van meer dan ééntwintigste procentpunt kan verkrijgen.De Geldgever is gerechtigd tijdens deze veertien dagen zijn voorstel aan eventuele wijzigingen in de marktomstandigheden aan te passen.]
        ";

        public string Article4FooterAllSections { get; } = @"  	Deze tekst opnemen indien Geldgever en Geldnemer renteaanpassing zijn overeengekomen en deze tekst van toepassing is.
  	Deze tekst opnemen indien artikel 4 lid 2 wordt opgenomen en Geldgever en Geldnemer daarmee akkoord gaan.";

        public string? Hoofdsom { get; set; }

        public string? Ingangsdatum { get; set; }

        public string? Einddatum { get; set; }

        public string? Notional { get; set; }

        public string? Coupondag { get; set; }

        public string? EersteCoupondatum { get; set; }

        public string? LaatsteCoupondatum { get; set; }

        public string? Aflossing { get; set; }

        public string? AantalAflossingen { get; set; }

        public string? AflossingsFrequentie { get; set; }

        public string? Rente { get; set; }

        public string? RenteConventie { get; set; }

        public string? Rentefrequentie { get; set; }

        public string? EmailContent { get; } =
        @"Dit tarief is 5 minuten handelbaar. Wij horen graag of u hier gebruik van wilt maken.

        Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door uw gevraagde bedrag dient te worden overgemaakt. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een geldleningsovereenkomst die door onze back office aan u zal worden toegestuurd.

        Op deze lening zullen de Algemene Voorwaarden NWB Bank van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden
        De NWB Bank kan pas tot storting van de lening overgaan nadat de geldleningsovereenkomst door alle partijen rechtsgeldig is ondertekend en door de NWB Bank is ontvangen. Mocht onverhoopt de ondertekening van de geldleningsovereenkomst niet tijdig rondkomen, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door de NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door de NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht. 

        Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen. 

        Bovenstaande is onder voorbehoud van typefouten.";
    }
}