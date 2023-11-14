using Hotels.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Data
{

    // class 
    //DbContext كلاس يرث كل خصائص الفريم وورك انتتي من 
    public class ApplicationDbContext:DbContext
    {
        // دالة بناء كونستراكتر 
        // الدالة التي يتم تشغيلها تلقائيا بمجرد استدعاء الكلاس او انشاء كائن
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //   DbContextOptions دالة بناء بالعربي داخلها كلاس اخر  لتسجيل جميع الكلاسات 
        {
            // الفائدة من دالة البناء هو تهيئة  البيانات 
            //  بما انه سيتم استدعاء الانتتي فريم وورك من اكثر من جزء في المشروع 
            //   لذلك انا في كل مرة لابد ان اهيئ الانتتي فريم وورك من جديد لذلك لابد اني اسوي ريفرش 
        
        }

       
        // انشات طبقة وسيطة
        public DbSet<Cart> carts { get; set; }  
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<RoomDetails> roomDetails { get; set; } 
        public DbSet<Rooms> rooms { get; set; }



    }
}
