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
        private static void showTotalFamilyMemebrs(List<people> people, List<StoreFamilyRelations> relations)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------- Total Family Members ------------------------------");
            Console.WriteLine();

            List<StoreFamilyRelations> families = new List<StoreFamilyRelations>();

            foreach(var familyMembers in relations)
            {
                if(familyMembers.Relation == "FAMILY")
                {
                    families.Add(new StoreFamilyRelations(familyMembers.Person, familyMembers.Relative, familyMembers.Relation));
                }
            }

            int count = 1;
            
            List<RepeatedMember> repeatedRecords = new List<RepeatedMember>();
            List<CountRelations> storeFamilyCount = new List<CountRelations>();

            CountRelations objToFindName;
            RepeatedMember objToFindEmail;
            RepeatedMember objToFindRelativeEmail;

            foreach (var person1 in families)
            {
                foreach(var person2 in families)
                {
                    objToFindEmail = repeatedRecords.Find(email => (email.Name == person2.Person));
                    objToFindRelativeEmail = repeatedRecords.Find(relativeEmail => (relativeEmail.Name == person2.Relative));

                    if(person1.Person == person2.Person && !repeatedRecords.Contains(objToFindEmail))
                    {
                        count++;
                        repeatedRecords.Add(new RepeatedMember(person2.Relative));
                    }
                    else if(person1.Person != person2.Person && !repeatedRecords.Contains(objToFindEmail) && repeatedRecords.Contains(objToFindRelativeEmail))
                    {
                        count++;
                        repeatedRecords.Add(new RepeatedMember(person2.Person));
                    }
                    
                }
                objToFindName = storeFamilyCount.Find(name => (name.Name == person1.Person));
                objToFindEmail = repeatedRecords.Find(email => (email.Name == person1.Person));

                if (!storeFamilyCount.Contains(objToFindName) && !repeatedRecords.Contains(objToFindEmail))
                {
                    storeFamilyCount.Add(new CountRelations(person1.Person, count));
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
            Console.WriteLine("------------------------- Total Relations With Each Other --------------------------");
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
           List<StoreFamilyRelations> storeFamily = new List<StoreFamilyRelations>();
           
           foreach (var person in people)
            {
                foreach(var relative in relations)
                {
                    foreach(var person1 in people)
                    {
                        if (person.email == relative.email && relative.emailOfRelative == person1.email)
                        {
                            Console.WriteLine(person.name + " and " + person1.name + " are " + relative.relationship);
                            storeFamily.Add(new StoreFamilyRelations(person.email, person1.email, relative.relationship));
                        }
                    }
                }
            }

            showTotalRelations(people , relations);
            showTotalFamilyMemebrs(people, storeFamily);

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
            
            
        }
    }
}
