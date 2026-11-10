using System;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DbHelper.InitializeDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
