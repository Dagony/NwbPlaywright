// using Castle.Core;
// using DocumentFormat.OpenXml.Drawing.Charts;
// using DocumentFormat.OpenXml.Drawing;
// using DocumentFormat.OpenXml.Office.MetaAttributes;
// using DocumentFormat.OpenXml.Wordprocessing;
// using Gherkin.CucumberMessages.Types;
// using NUnit.Framework;
// using System.Reflection;
// using System.Runtime.ConstrainedExecution;
// using System;
// Examples: test


// Given I enter data
//      | Name   | MiddleName   | LastName   | Preposition   | DateOfBirth   | GenderType   | CountryOfOrigin   | CountryOfCitizenship   | CountryOfResidency   |
//      | <Name> | <MiddleName> | <LastName> | <Preposition> | <DateOfBirth> | <GenderType> | <CountryOfOrigin> | <CountryOfCitizenship> | <CountryOfResidency> |

// | Name | MiddleName | LastName | Preposition | DateOfBirth | GenderType | CountryOfOrigin | CountryOfCitizenship | CountryOfResidency |

// | Trx Abed |                    | Nadir |               | 18 - 06 - 1989 | Male | United States | United States | Netherlands |

// | Trx Annie |                    | Edison |               | 05 - 12 - 1988 | Female | United States | United States | Netherlands |

// | Trx Shirley |                    | Bennet |               | 12 - 09 - 1970 | Female | United States | United States | Netherlands |

// | Trx Troy |                    | Barnes |               | 04 - 05 - 1990 | Male | United States | United States | Netherlands |

// | Trx Pierce |                    | Hawthorne |               | 15 - 11 - 1944 | Male | United States | United States | Netherlands |

// | Trx Benjamin | Hoon | Chang | von | 03 - 07 - 1971 | Male | South Korea | United States | Netherlands |

// | Trx Jeff |                    | Winger |               | 01 - 03 - 1975 | Male | United States | United States | Netherlands |

// | Trx Britta |                    | Perry |               | 22 - 10 - 1980 | Female | Netherlands |                      |                    |

// | Trx Christopher |                    | Turk |               | 05 - 03 - 1984 | Male | United States | United States | Netherlands |

// | Trx Perry |                    | Cox |               | 10 - 09 - 1979 | Male | United States | United States | Netherlands |

// | Trx John |                    | Dorian |               | 15 - 01 - 1989 | Male | United States | United States | Netherlands |

// | Trx Elliot |                    | Reid |               | 22 - 07 - 1992 | Female | Netherlands |                      |                    |

// | Trx Carla |                    | Espinosa |               | 03 - 04 - 1982 | Female | Dominican Republic | Netherlands | Netherlands |

// | Trx Bob |                    | Kelso |               | 20 - 05 - 1957 | Male | United States | United States | Netherlands |

// | Trx Todd | Benjamin | Quinlan | von | 08 - 12 - 1991 | Male | United States | United States | Netherlands |

// | Trx Ludo |                    | Sanders |               | 10 - 05 - 1965 | Male | Netherlands |                      |                    |

// | Trx Janine |                    | Elschot |               | 25 - 09 - 1980 | Female | Netherlands |                      |                    |

// | Trx Nina |                    | Sanders |               | 03 - 03 - 1972 | Female | Netherlands |                      |                    |

// | Trx Maxime |                    | Sanders |               | 15 - 07 - 1990 | Female | Netherlands |                      |                    |

// | Trx Anton |                    | Bouwhuis |               | 20 - 12 - 1960 | Male | Netherlands |                      |                    |

// | Trx Bing |                    | Mauricius |               | 08 - 04 - 1985 | Male | Netherlands |                      |                    |

// | Trx Sjoerd |                    | Bouwhuis |               | 17 - 11 - 1978 | Male | Netherlands |                      |                    |

// | Trx Jessica | Maria | Hamsen | de | 06 - 04 - 1988 | Female | Netherlands |                      |                    |

// | Trx Arnie |                    | Alberts |               | 20 - 09 - 1988 | Male | Netherlands |                      |                    |

// | Trx Alfred | Jodocus | Kwak |               | 01 - 04 - 1989 | Male | Netherlands |                      |                    |

// | Trx Henk |                    | Mol | de | 15 - 07 - 1975 | Male | Netherlands |                      |                    |

// | Trx Dolf |                    | Kraai | de | 10 - 03 - 1968 | Male | Netherlands |                      |                    |

// | Trx Winnie |                    | Wanna |               | 20 - 05 - 1990 | Female | Netherlands |                      |                    |

// | Trx Pikkie |                    | Ekster | de | 03 - 09 - 1992 | Female | Netherlands |                      |                    |

// | Trx Ollie |                    | Ooievaar | de | 25 - 12 - 1980 | Unspecified | Netherlands |                      |                    |

// | Trx Professor | Paljas | Pinguïn | von | 19 - 07 - 1981 | Male | Netherlands |                      |                    |

// | Trx Michael |                    | Scott |               | 23 - 03 - 1965 | Male | United States | United States | Netherlands |

// | Trx Jim |                    | Halpert |               | 01 - 10 - 1978 | Male | United States | United States | Netherlands |

// | Trx Dwight |                    | Schrute |               | 20 - 01 - 1972 | Male | United States | United States | Netherlands |

// | Trx Pam |                    | Beesly |               | 25 - 03 - 1979 | Female | United States | United States | Netherlands |

// | Trx Ryan |                    | Howard |               | 05 - 06 - 1980 | Male | United States | United States | Netherlands |

// | Trx Angela |                    | Martin |               | 30 - 11 - 1976 | Female | United States | United States | Netherlands |

// | Trx Stanley |                    | Hudson |               | 19 - 02 - 1958 | Male | United States | United States | Netherlands |

// | Trx Kelly | Chrissy | Kapoor | van der | 05 - 02 - 1986 | Female | United States | United States | Netherlands |

// | Trx Kevin |                    | Malone |               | 15 - 05 - 1970 | Male | United States | United States | Netherlands |

// | Trx Jimmy |                    | McGill |               | 23 - 04 - 1967 | Male | Netherlands |                      |                    |

// | Trx Kim |                    | Wexler |               | 25 - 09 - 1980 | Female | Netherlands |                      |                    |

// | Trx Mike |                    | Ermantraut |               | 04 - 02 - 1956 | Male | Germany | United States | Netherlands |

// | Trx Chuck |                    | McGill |               | 10 - 09 - 1952 | Male | Netherlands |                      |                    |

// | Trx Nacho |                    | Varga |               | 30 - 06 - 1985 | Male | Netherlands |                      |                    |

// | Trx Howard |                    | Hamlin |               | 12 - 09 - 1969 | Male | Netherlands |                      |                    |

// | Trx Hector |                    | Salamanca |               | 15 - 04 - 1940 | Male | Netherlands |                      |                    |

// | Trx Lalo | Fernando Alejandro | Salamanca | de los Santos | 03-11-1977  | Male        | Netherlands        |                      |                    |
//  | Trx Tyrion      |                    | Lannister  |               | 11-04-1978  | Male        | Netherlands        |                      |                    |
//  | Trx Cersei      |                    | Lannister  |               | 03-12-1975  | Female      | Netherlands        |                      |                    |
//  | Trx Jaime       |                    | Lannister  |               | 03-12-1975  | Male        | Netherlands        |                      |                    |
//  | Trx Joffrey     |                    | Baratheon  |               | 12-02-1997  | Male        | Netherlands        |                      |                    |
//  | Trx Tywin       |                    | Lannister  |               | 25-10-1944  | Male        | Netherlands        |                      |                    |
//  | Trx Myrcella    |                    | Baratheon  |               | 12-02-1997  | Female      | Netherlands        |                      |                    |
//  | Trx Tommen      | Robert             | Baratheon  | van der       | 26-03-1999  | Male        | Netherlands        |                      |                    |
//  | Trx Daan        |                    | Janssen    |               | 15-05-1990  | Male        | Netherlands        |                      |                    |
//  | Trx Lotte       |                    | Berg       | van der       | 28-11-1988  | Female      | Netherlands        |                      |                    |
//  | Trx Thijs       |                    | Vries      | de            | 10-03-1995  | Male        | Netherlands        |                      |                    |
//  | Trx Sanne       |                    | Dijk       | van           | 12-02-1997  | Female      | Netherlands        |                      |                    |
//  | Trx Bram        |                    | Bakker     |               | 03-09-1986  | Male        | Netherlands        |                      |                    |
//  | Trx Sophie      |                    | Mulder     |               | 17-02-1993  | Female      | Netherlands        |                      |                    |
//  | Trx Eva         |                    | Leeuwen    | van           | 25-06-1987  | Female      | Netherlands        |                      |                    |
//  | Trx Ruben       |                    | Jong       | de            | 09-12-1994  | Male        | Netherlands        |                      |                    |
//  | Trx Julia       |                    | Heuvel     | van den       | 30-04-1990  | Female      | Netherlands        |                      |                    |
//  | Trx Lisa        |                    | Smit       |               | 22-03-1996  | Female      | Netherlands        |                      |                    |
//  | Trx Tom         |                    | Dijk       | van           | 14-07-1985  | Male        | Netherlands        |                      |                    |
//  | Trx Lars        |                    | Meer       | van der       | 08-05-1989  | Male        | Netherlands        |                      |                    |