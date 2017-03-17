namespace Sixteenth_day
{
    class Sue
    {
        public int Children { get; set; }
        public int Cats { get; set; }
        public int Samoyeds { get; set; }
        public int Pomeranians { get; set; }
        public int Akitas { get; set; }
        public int Vizslas { get; set; }
        public int Goldfish { get; set; }
        public int Trees { get; set; }
        public int Cars { get; set; }
        public int Perfumes { get; set; }
        public Sue(int children, int cats, int samoyeds, int pomeranians, int akitas, int vizslas, int goldfish, int trees, int cars, int perfumes)
        {
            Children = children;
            Cats = cats;
            Samoyeds = samoyeds;
            Pomeranians = pomeranians;
            Akitas = akitas;
            Vizslas = vizslas;
            Goldfish = goldfish;
            Trees = trees;
            Cars = cars;
            Perfumes = perfumes;
        }
        public int Points(int children, int cats, int samoyeds, int pomeranians, int akitas, int vizslas, int goldfish, int trees, int cars, int perfumes)
        {
            int p = 0;
            if (children == Children)
                p++;
            else if (Children != -1)
                p--;
            if (cats < Cats)
                p++;
            else if (Cats != -1)
                p--;
            if (samoyeds == Samoyeds)
                p++;
            else if (Samoyeds != -1)
                p--;
            if (pomeranians > Pomeranians)
                p++;
            else if (Pomeranians != -1)
                p--;
            if (akitas == Akitas)
                p++;
            else if (Akitas != -1)
                p--;
            if (vizslas == Vizslas)
                p++;
            else if (Vizslas != -1)
                p--;
            if (goldfish > Goldfish)
                p++;
            else if (Goldfish != -1)
                p--;
            if (trees < Trees)
                p++;
            else if (Trees != -1)
                p--;
            if (cars == Cars)
                p++;
            else if (Cars != -1)
                p--;
            if (perfumes == Perfumes)
                p++;
            else if (Perfumes != -1)
                p--;
            return p;
        }
    }
}
