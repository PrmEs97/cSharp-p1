using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class PersonalInformation
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }

    public override string ToString()
    {
        return $"نام: {Name}، سن: {Age}، آدرس: {Address}";
    }
}

class PersonalInformationOrganizer
{
    private List<PersonalInformation> personalInfoList;

    public PersonalInformationOrganizer()
    {
        personalInfoList = new List<PersonalInformation>();
        LoadData();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("سازماندهی اطلاعات شخصی");
            Console.WriteLine("1. افزودن اطلاعات شخصی");
            Console.WriteLine("2. مشاهده همه اطلاعات شخصی");
            Console.WriteLine("3. ذخیره و خروج");

            Console.Write("انتخاب کنید (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    افزودناطلاعاتشخصی();
                    break;
                case "2":
                    مشاهدههمهاطلاعاتشخصی();
                    break;
                case "3":
                    ذخیره_و_خروج();
                    break;
                default:
                    Console.WriteLine("انتخاب نامعتبر. لطفاً عددی بین 1 و 3 وارد کنید.");
                    break;
            }
        }
    }

    private void افزودناطلاعاتشخصی()
    {
        PersonalInformation person = new PersonalInformation();

        Console.Write("نام را وارد کنید: ");
        person.Name = Console.ReadLine();

        Console.Write("سن را وارد کنید: ");
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            person.Age = age;
        }
        else
        {
            Console.WriteLine("سن نامعتبر است. لطفاً یک عدد معتبر وارد کنید.");
            return;
        }

        Console.Write("آدرس را وارد کنید: ");
        person.Address = Console.ReadLine();

        personalInfoList.Add(person);
        Console.WriteLine("اطلاعات شخصی با موفقیت اضافه شد!\n");
    }

    private void مشاهدههمهاطلاعاتشخصی()
    {
        if (personalInfoList.Count == 0)
        {
            Console.WriteLine("هیچ اطلاعات شخصی موجود نیست.\n");
            return;
        }

        Console.WriteLine("همه اطلاعات شخصی:\n");

        foreach (var person in personalInfoList)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine();
    }

    private void ذخیره_و_خروج()
    {
        string jsonData = JsonConvert.SerializeObject(personalInfoList);

        try
        {
            File.WriteAllText("personalInfo.json", jsonData);
            Console.WriteLine("داده‌ها با موفقیت ذخیره شد.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"خطا در ذخیره داده‌ها: {ex.Message}");
        }

        Environment.Exit(0);
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists("personalInfo.json"))
            {
                string jsonData = File.ReadAllText("personalInfo.json");
                personalInfoList = JsonConvert.DeserializeObject<List<PersonalInformation>>(jsonData);
                Console.WriteLine("داده‌ها با موفقیت بارگیری شد.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"خطا در بارگیری داده‌ها: {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        PersonalInformationOrganizer organizer = new PersonalInformationOrganizer();
        organizer.Run();
    }
}
