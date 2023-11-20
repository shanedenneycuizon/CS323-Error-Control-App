string choice = "";
string func = "";
int row = 0;
int col = 0;


Console.WriteLine("Welcome to my Error Control App");
do {
    Console.WriteLine("Would you like to continue using the application?\n1 - Yes\n2 - No");
    Console.Write("Choice: ");
    choice = Console.ReadLine();
    if(choice == "1" || choice.ToLower() == "yes")
    {
        Console.WriteLine("Choose a function you'd like to use:");
        Console.WriteLine("\t1 - Check if set of bits are ODD");
        Console.WriteLine("\t2 - Check if set of bits are EVEN");
        Console.WriteLine("\t3 - Find BCC");
        Console.WriteLine("\t4 - Find BCC and check errors");
        Console.WriteLine("\t5 - Find BCC, ODD/EVEN-sets, & Errors");
        Console.WriteLine("\t6 - Exit");

        Console.Write("\nFunction choice: ");
        func = Console.ReadLine();
        switch(func)
        {
            case "1": CheckODD();
                break;
            case "2": CheckEVEN();
                break;
            case "3":
                int[] bcc = findBCC(inputBlock());
                //print BCC
                Console.Write("BCC:   ");
                foreach (int bit in bcc)
                {
                    Console.Write(bit + " ");
                }
                Console.WriteLine("");
                break;
            case "4": BlockBitsErrorCheck();
                break;
            case "5": BlockBitsAndParityErrorCheck();
                break;
        }
    }
    else { break; }

}while (choice=="1" || choice.ToLower() == "yes"||(Convert.ToInt32(func) > 0 && Convert.ToInt32(func) < 6));

Console.WriteLine("Thank you for checking out my application!");

//Block Input
static int[,] inputBlock()
{
    Console.Write("Input number of rows: ");
    int row = Convert.ToInt32(Console.ReadLine());
    Console.Write("Input number of colums: ");
    int col = Convert.ToInt32(Console.ReadLine());

    int[,] block = new int[row, col];
    Console.WriteLine("Input the bits separated by a space.");
    for (int i = 0; i < row; i++)
    {
        Console.Write("Row " + (i + 1) + ": ");
        //input the bits
        string bits = Console.ReadLine();
        //separate the inputed string
        int[] split_bits = Array.ConvertAll(bits.Split(), int.Parse);

        for (int j = 0; j < col; j++)
        {
            block[i, j] = split_bits[j];
        }
    }
    return block;
}

//a function that will input a set of bits and set whether or not it is ODD
static void CheckODD()
{
    string message = "No error.";
    Console.WriteLine("Input the bits separated by a space.");
    
    //input the bits
    string bits = Console.ReadLine();
    
    //separate the inputed string
    int[] split_bits = Array.ConvertAll(bits.Split(), int.Parse);
    
    //count ones
    int ones = 0;
    foreach (int bit in split_bits) { if (bit == 1) { ones++; } }
    if(ones%2 == 0) { message = "Error. Bits are even."; }
    else { message = "Positive. Bits are odd."; }

    Console.WriteLine(message);
}
//a function that will input a set of bits and set whether or not it is EVEN
static void CheckEVEN()
{
    string message = "No error.";
    Console.WriteLine("Input the bits separated by a space.");

    //input the bits
    string bits = Console.ReadLine();

    //separate the inputed string
    int[] split_bits = Array.ConvertAll(bits.Split(), int.Parse);

    //count ones
    int ones = 0;
    foreach (int bit in split_bits) { if (bit == 1) { ones++; } }
    if (ones % 2 == 0) { message = "Positive. Bits are even."; }
    else { message = "Error. Bits are odd."; }

    Console.WriteLine(message);
}
//a function that will input a block of character bits and will find its BCC
static int[] findBCC (int[,] block)
{
    
    int row = block.GetLength(0);
    int col = block.GetLength(1);
    int[] bcc = new int[col];
    for (int col_ctr = 0; col_ctr < col; col_ctr++)
    {
        int ones = 0;
        for (int row_ctr = 0; row_ctr < row; row_ctr++)
        {
            if (block[row_ctr, col_ctr] == 1)
            {
                ones++;
            }
        }
        if(ones % 2 == 0)
        {
            bcc[col_ctr] = 0;
        }
        else
        {
            bcc[col_ctr] = 1;
        }
    }
    
    return bcc;
}

//a function that will input a block of character bits and its BCC and checks if it has an  error or not
static void BlockBitsErrorCheck()
{
    int[,] block = inputBlock();
    //input the BCCbits
    Console.Write("BCC:   ");
    string BCCbits = Console.ReadLine();

    //separate the inputed string
    int[] split_BCCbits = Array.ConvertAll(BCCbits.Split(), int.Parse);
    int[] correctBCC = findBCC(block);
    if (!correctBCC.SequenceEqual(split_BCCbits))
    {
        Console.WriteLine("Error. BCC Incorrect.");
        Console.WriteLine("BCC should be:");
        Console.Write("BCC:   ");
        foreach (int bit in correctBCC)
        {
            Console.Write(bit + " ");
        }
        Console.WriteLine("");
    }
    else
    {
        Console.WriteLine("Positive. BCC correct.");
    }
}

//a function that will input a block of character bits and will find its BCC through LRC
//EVEN-set or ODD-set parity bits and the parity bit of the BCC
static void BlockBitsAndParityErrorCheck()
{
    int[,] block = inputBlock();
    int row = block.GetLength(0);
    int col = block.GetLength(1);
    int[] BCC = findBCC(block);
    
    //ODD Parity
    Console.WriteLine("ODD Parity Bit:");
    //count 1s in each row
    int[] oddpar = new int[row];
    for (int row_ctr = 0; row_ctr < row; row_ctr++)
    {
        int ones = 0;
        for (int col_ctr = 0; col_ctr < col; col_ctr++)
        {
            if (block[row_ctr, col_ctr] == 1) { ones++; }
        }
        if (ones % 2 == 0) { oddpar[row_ctr] = 1; }
        else { oddpar[row_ctr] = 0; }
    }
    foreach (int odd in oddpar) { Console.WriteLine(odd); }

    //EVEN Parity
    Console.WriteLine("EVEN Parity Bit:");
    //count 1s in each row
    int[] evenpar = new int[row];

    for (int row_ctr = 0; row_ctr < row; row_ctr++)
    {
        int ones = 0;
        for (int col_ctr = 0; col_ctr < col; col_ctr++)
        {
            if (block[row_ctr, col_ctr] == 1) { ones++; }
        }
        if (ones % 2 == 0) { evenpar[row_ctr] = 0; }
        else { evenpar[row_ctr] = 1; }

    }
    foreach (int even in evenpar) { Console.WriteLine(even); }
    
    Console.Write("BCC: ");
    foreach (int bit in BCC) { Console.Write(bit + " "); }
    Console.WriteLine("");
}