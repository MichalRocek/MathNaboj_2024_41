//Michal Roček, úloha 41 ze soutěže Matematický náboj 2024, aproximace výsledku pomocí simulace, 26. 04. 2024
//Zadání úlohy:
/*
 Máme dvě krabice, první obsahuje pět spolehlivých žárovek a devět nespolehlivých, druhá krabice obsahuje devět spolehlivých a pět nespolehlivých. Spolehlivé žárovky fungují vždy, nespolehlivé fungují s pravděpodobností p (kde 0 < p < 1) stejnou pro všechny nespolehlivé žárovky. Najděte hodnotu p, pro kterou jsou následující dva jevy stejně pravděpodobné.
1. Náhodně vybraná žárovka z první krabice funguje.
2. Dvě náhodně vybrané žárovky z druhé krabice obě fungují.
*/
using MathNaboj_2024_41;


//Testování třídy Random
/*
//Příprava proměnných
Random rng = new Random();
const int testRange = 1_000_000_000; //nastavitelný rozsah testu (na kolika datech proběhne)
const int rngSpread = 14; //nastavitelný rozsah dat
int[] results = new int[rngSpread];
for (int i = 0; i < results.Length; i++) results[i] = 0;

//Test
for (int i = 0; i < testRange; i++)
{
    results[rng.Next(rngSpread)] += 1;
}

//Výpis testu
results.Print();
*/



//Aproximace pravděpodobnosti

//příprava krabic
bool[] box1 = new bool[14] //true = spolehlivá žárovka, false = nespolehlivá žárovka
{
    true,
    true,
    true,
    true,
    true,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false
};
bool[] box2 = new bool[14]
{
    false,
    false,
    false,
    false,
    false,
    true,
    true,
    true,
    true,
    true,
    true,
    true,
    true,
    true
};
Random rng = new Random();
const long testRange = 10_000_000_000; //nastavitelný rozsah testu (na kolika datech proběhne)
const int bulbCount = 14; //počet žárovek v krabici

//Počty vytažených žárovek z jednotlivých krabic dle typu
long box1Good = 0;
long box1NoGood = 0;
long box2BothGood = 0;
long box2HalfGood = 0;
long box2NoGood = 0;

//Pozice vybraných žárovek v druhé krabici
int firstBulb;
int secondBulb;

//Aproximující program
for (long i = 0; i < testRange; i++)
{
    //vytažení žárovky z první krabice
    if (box1[rng.Next(bulbCount)]) box1Good++;
    else box1NoGood++;

    //vytažení žárovek z druhé krabice
    firstBulb = rng.Next(bulbCount);
    do
    {
        secondBulb = rng.Next(bulbCount);
    }
    while (secondBulb == firstBulb); //nelze vytáhnout jednu žárovku dvakrát

    if (box2[firstBulb] && box2[secondBulb]) box2BothGood++;
    else if (!box2[firstBulb] && !box2[secondBulb]) box2NoGood++;
    else box2HalfGood++;
}
//String.Format("box1Good: {0}\nbox1NoGood: {1}", box1Good, box1NoGood).Print();
//String.Format("box2BothGood: {0}\nbox2HalfGood: {1}\nbox2NoGood: {2}", box2BothGood, box2HalfGood, box2NoGood).Print();

//Výpočet pravděpodobnosti p
long a = box2NoGood;
long b = box2HalfGood - box1NoGood;
long c = box2BothGood - box1Good;
a.Print();b.Print();c.Print();
double discriminant = b * b - 4 * a * c;
discriminant.Print("Diskriminant: ");
Math.Sqrt(discriminant).Print();
double p1 = (- b - Math.Sqrt(discriminant)) / (2 * a);
double p2 = (- b + Math.Sqrt(discriminant)) / (2 * a);
String.Format("Pravděpodobnost p1: {0}", p1).Print();
String.Format("Pravděpodobnost p2: {0}", p2).Print();