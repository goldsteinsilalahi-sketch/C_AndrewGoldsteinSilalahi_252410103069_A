using System;
public class Sekolah
{
    public string idSekolah;
    public string namaSekolah;
    public int JumlahPaketDiterima;

    public Sekolah(string idSekolah, string namaSekolah, int JumlahPaketDiterima)
    {
        this.idSekolah = idSekolah;
        this.namaSekolah = namaSekolah;
        this.JumlahPaketDiterima = JumlahPaketDiterima;
    }

    public void TampilInfo()
    {
        Console.WriteLine($"Id Sekolah: {idSekolah}");
        Console.WriteLine($"Nama Sekolah: {namaSekolah}");
        Console.WriteLine($"Jumlah Paket Diterima: {JumlahPaketDiterima}");
    }
}
    
public class PaketMBG
{
    private string idPaket { get; set; }
    private string namaPaket { get; set; }
    private int stokDapur;
    public int StokDapur
    {
        get { return stokDapur; }
        set
        {
            if(value >= 0)
            {
                stokDapur = value;
            }
            else
            {
                Console.WriteLine("nilai nya gak bisa minus le");
            }
        }
    }
    public PaketMBG() { }

    public PaketMBG(string idPaket,string namaPaket, int StokDapur)
    {
        this.idPaket = idPaket;
        this.namaPaket = namaPaket;
        this.StokDapur = StokDapur;
    }

    public void KurangiStok(int jumlah)
    {
        if(jumlah <= StokDapur)
        {
            StokDapur -= jumlah;
            Console.WriteLine($"Stok Dapur Sekarang: {StokDapur}");
        } else
        {
            Console.WriteLine("Stok tidak mencukupi");
        }
    }

    public virtual void HitungBiaya(int jumlah) { }
}

public class PaketReguler : PaketMBG
{
    public int Harga=15000;
    public string Makanan;
    //public string List<Makanan>();
    public PaketReguler(string idPaket, string namaPaket, int StokDapur) : base (idPaket, namaPaket, StokDapur) { }

    public override void HitungBiaya(int jumlah)
    {
        int Biaya=jumlah* Harga;
        Console.WriteLine($"Biaya yang perlu dibayar: {Biaya}");
    }
}

public class PaketKhusus : PaketMBG
{
    public int Harga = 20000;
    public string jenisDiet;
    //PaketKhusus paket1 = new PaketKhusus();
    public PaketKhusus(string idPaket, string namaPaket, int StokDapur) : base(idPaket, namaPaket, StokDapur) { }
    public override void HitungBiaya(int jumlah)
    {
        int Biaya = jumlah * Harga;
        Console.WriteLine($"Biaya yang perlu dibayar: {Biaya}");
    }
}

interface DapatDidistribusikan
{
    public void KirimPaket() { }
}

class DistribusiMBG : DapatDidistribusikan
{
    public Sekolah Tujuan;
    public PaketMBG PaketMakanan;
    public int Jumlah;
    //{ get; set { if(value > 0) Jumlah =value ; } }

    public DistribusiMBG(Sekolah Tujuan, PaketMBG PaketMakanan, int Jumlah)
    {
        this.Tujuan = Tujuan;
        this.PaketMakanan = PaketMakanan;
        this.Jumlah = Jumlah;
    }
    public void KirimPaket()
    {
        if(Jumlah > 0 && Jumlah <= PaketMakanan.StokDapur)
        {
            PaketMakanan.KurangiStok(Jumlah);
            Tujuan.JumlahPaketDiterima += Jumlah;
            Console.WriteLine($"Distribusi MBG Berhasil! Sebanyak {Jumlah} paket makanan telah dikirim ke sekolah {Tujuan.namaSekolah}");
        } else if(Jumlah <= 0)
        {
            Console.WriteLine("Distribusi gagal karena jumlah distribusi kurang dari 1");
            Tujuan.TampilInfo();
        } else if (Jumlah > PaketMakanan.StokDapur)
        {
            Console.WriteLine("Distribusi gagal karena stok dapur tidak menyanggupi jumlah distribusi");
            Tujuan.TampilInfo();
        }
    }
}

public class PengelolaMBG
{
    //aku lupa cara membuat list kak, jadii gini aja
    public void TambahSekolah() { }
    public void TambahPaket() { }
    public void TampilkanDataMBG() { }
}
//Hubungan antara PengelolaMBG dengan class Sekolah adalah aggregation.
//karena PengelolaMBG tidak dapat berdiri sendiri tanpa ada Sekolah,
//sementara Sekolah dapat berdiri sendiri tanpa ada PengelolaMBG

class MainApp
{
    static void Main(string[] args)
    {
        Console.WriteLine("======Class Sekolah======");
        Sekolah sekolah1 = new Sekolah("001", "Pelatihan pengendali angin", 500);
        sekolah1.TampilInfo();
        Sekolah sekolah2 = new Sekolah("002", "Tadika Mesra", 350);
        Console.WriteLine("======Poly Morphhism=====");
        PaketReguler paket1R = new PaketReguler("003","paket gokil", 600);
        paket1R.HitungBiaya(19);
        PaketKhusus paket1K = new PaketKhusus("004","paket sehat",380);
        paket1K.HitungBiaya(70);
        Console.WriteLine("======Interface======");
        DistribusiMBG dis1 = new DistribusiMBG(sekolah1, paket1R, 10000);
        dis1.KirimPaket();
    }
}