using Newtonsoft.Json;
using PYP_Task_VCard_Simple_Project;
using System.Reflection;

var client = new HttpClient();
client.BaseAddress = new Uri("https://randomuser.me/");

var response = client.GetAsync("api?results=50").Result;
var responseString = response.Content.ReadAsStringAsync().Result;
var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);

var vCards = new List<VCard>();

foreach (var result in responseJson.results)
{
    var vCard = new VCard();
    vCard.Firtname = result.name.first;
    vCard.Surname = result.name.last;
    vCard.Email = result.email;
    vCard.Phone = result.phone;
    vCard.City = result.location.city;
    vCard.Country = result.location.country;
    vCards.Add(vCard);

    // Generate Qr code 
    string path = @"C:\Users\tabri\OneDrive\Desktop\PYP_Task_VCard_Simple_Project\PYP_Task_VCard_Simple_Project\QRCode" + vCard.Firtname + vCard.Surname + ".txt";
    using (StreamWriter sw = File.CreateText(path))
    {
        sw.WriteLine("BEGIN:VCARD");
        sw.WriteLine("VERSION:2.1");
        sw.WriteLine("N:;{0};{1};;;", vCard.Firtname, vCard.Surname);
        sw.WriteLine("FN:{0} {1}", vCard.Firtname, vCard.Surname);
        sw.WriteLine("TEL;CELL:{0}", vCard.Phone);
        sw.WriteLine("EMAIL;PREF;INTERNET:{0}", vCard.Email);
        sw.WriteLine("ADR;HOME:;;{0};{1}", vCard.City, vCard.Country);
        sw.WriteLine("END:VCARD");
    }
}

using (var db = new PYP_Task_VCard_DbContext())
{
    db.vCards.AddRange(vCards);
    db.SaveChanges();
}
