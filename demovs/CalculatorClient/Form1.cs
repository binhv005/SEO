using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using ServiceReference; // Khớp với namespace trong Reference.cs

namespace CalculatorClient
{
    public partial class Form1 : Form
    {
        private TextBox txt1 = new TextBox { Location = new Point(50, 20), Width = 150 };
        private TextBox txt2 = new TextBox { Location = new Point(50, 60), Width = 150 };
        private Label lblRes = new Label { Location = new Point(50, 150), Text = "Kết quả: ", Width = 250, AutoSize = true };

        public Form1()
        {
            InitializeComponent();
            this.Text = "SOAP Calculator";
            this.Width = 350;
            this.Height = 250;

            this.Controls.Add(txt1); 
            this.Controls.Add(txt2); 
            this.Controls.Add(lblRes);

            string[] ops = { "+", "-", "*", "/" };
            for (int i = 0; i < ops.Length; i++)
            {
                Button btn = new Button { 
                    Text = ops[i], 
                    Location = new Point(50 + (i * 55), 100), 
                    Width = 50, 
                    Tag = ops[i] 
                };
                btn.Click += async (s, e) => {
                    string? op = (s as Button)?.Tag?.ToString();
                    if (op != null) await ExecuteCalc(op);
                };
                this.Controls.Add(btn);
            }
        }

        private async Task ExecuteCalc(string op)
        {
            try
            {
                // Khởi tạo client sử dụng cấu hình mặc định từ Proxy
                var client = new CalculatorWsClient();
                
                if (!int.TryParse(txt1.Text, out int a) || !int.TryParse(txt2.Text, out int b))
                {
                    MessageBox.Show("Vui lòng nhập số nguyên hợp lệ!");
                    return;
                }

                double finalResult = 0;

                // Gọi các hàm Async dựa trên cấu trúc arg0, arg1 trong Reference.cs
                switch (op)
                {
                    case "+": 
                        var addRes = await client.addAsync(a, b);
                        finalResult = addRes.@return; 
                        break;
                    case "-": 
                        var subRes = await client.subtractAsync(a, b);
                        finalResult = subRes.@return; 
                        break;
                    case "*": 
                        var mulRes = await client.multiplyAsync(a, b);
                        finalResult = mulRes.@return; 
                        break;
                    case "/": 
                        if (b == 0) { MessageBox.Show("Không thể chia cho 0"); return; }
                        var divRes = await client.divideAsync(a, b);
                        finalResult = divRes.@return; 
                        break;
                }

                lblRes.Text = "Kết quả từ Java: " + finalResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Web Service: " + ex.Message);
            }
        }
    }
}