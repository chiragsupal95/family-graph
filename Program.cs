using System;
using System.Linq;
using LINQtoCSV;
using System.Collections.Generic;

namespace FamilyGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            readCsvFiles();
            Console.ReadKey();
        }
        private static void showTotalFamilyMemebrs(List<people> people, List<relations> relations)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------- Total Family Members ------------------------------");
            Console.WriteLine();

            int count = 1;
            
            List<RepeatedMember> repeatedRecords = new List<RepeatedMember>();
            List<CountRelations> storeFamilyCount = new List<CountRelations>();
            CountRelations objToFindName;
            RepeatedMember objToFindEmail;
            RepeatedMember objToFindRelativeEmail;

            foreach (var person1 in relations)
            {
                foreach(var person2 in relations)
                {
                    objToFindEmail = repeatedRecords.Find(email => (email.Name == person2.email));
                    objToFindRelativeEmail = repeatedRecords.Find(relativeEmail => (relativeEmail.Name == person2.emailOfRelative));

                    if(person1.email == person2.email && !repeatedRecords.Contains(objToFindEmail) && person1.relationship == "FAMILY")
                    {
                        count++;
                        repeatedRecords.Add(new RepeatedMember(person2.emailOfRelative));
                    }
                    else if(person1.email != person2.email && !repeatedRecords.Contains(objToFindEmail) && repeatedRecords.Contains(objToFindRelativeEmail) && person1.relationship == "FAMILY")
                    {
                        count++;
                        repeatedRecords.Add(new RepeatedMember(person2.email));
                    }
                    
                }
                objToFindName = storeFamilyCount.Find(name => (name.Name == person1.email));
                objToFindEmail = repeatedRecords.Find(email => (email.Name == person1.email));

                if (!storeFamilyCount.Contains(objToFindName) && !repeatedRecords.Contains(objToFindEmail))
                {
                    storeFamilyCount.Add(new CountRelations(person1.email, count));
                }
                count = 1;
            } 

            foreach(var records in people)
            {
                foreach(var person in storeFamilyCount)
                {
                    if(records.email == person.Name)
                    {
                        Console.WriteLine(records.name + " : " + person.Count);
                    }
                }
            }
         }

        private static void showTotalRelations(List<people> people, List<relations> relations)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------- Total Relations With Each Other ------------------------------");
            Console.WriteLine();
            
            List<CountRelations> storeCount = new List<CountRelations>();
            int count = 0;
            foreach(var person in people)
            {
                foreach(var relative in relations)
                {
                    if(person.email == relative.email || person.email == relative.emailOfRelative)
                    {
                        count++;
                    }
                }
                storeCount.Add(new CountRelations (person.name,count));
                count = 0;
            }

            foreach(var record in storeCount)
            {
                Console.WriteLine(record.Name + " : " + record.Count);
            }
        }

        private static void checkRelations(List<people> people, List<relations> relations)
        {
           foreach (var person in people)
            {
                foreach(var relative in relations)
                {
                    foreach(var person1 in people)
                    {
                        if (person.email == relative.email && relative.emailOfRelative == person1.email)
                        {
                            Console.WriteLine(person.name + " and " + person1.name + " are " + relative.relationship);
                        }
                    }
                }
            }
            
        }

        private static void readCsvFiles()
        {
            var csvFile = new CsvFileDescription
            {
                FirstLineHasColumnNames = false,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = true,
                EnforceCsvColumnAttribute = true
            };

            var csvContext = new CsvContext();

            List<people> peopleCsv = csvContext.Read<people>("people.csv", csvFile).ToList();
            List<relations> relationshipsCsv = csvContext.Read<relations>("relationships.csv", csvFile).ToList();

            checkRelations(peopleCsv,relationshipsCsv);
            showTotalRelations(peopleCsv, relationshipsCsv);
            showTotalFamilyMemebrs(peopleCsv, relationshipsCsv);
        }
    }
}
