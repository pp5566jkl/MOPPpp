namespace MOPPpp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "圖像文件(JPeg, Gif, Bmp, etc.)|.jpg;*jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png|所有文件(*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Bitmap MyBitmap = new Bitmap(openFileDialog1.FileName);
                    this.pictureBox1.Image = MyBitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }

            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newBitmap = new Bitmap(Width, Height);
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = oldBitmap.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        Result = (299 * r + 587 * g + 114 * b) / 1000;
                        newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
                this.pictureBox1.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newBitmap = new Bitmap(Width, Height);
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;
                int[] pixel_mask = new int[9];
                int pixS;

                int[] Smoothing = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                for (int x = 1; x < Width - 1; x++)
                    for (int y = 1; y < Height - 1; y++)
                    {
                        pixel_mask[0] = oldBitmap.GetPixel(x - 1, y - 1).G;
                        pixel_mask[1] = oldBitmap.GetPixel(x, y - 1).G;
                        pixel_mask[2] = oldBitmap.GetPixel(x + 1, y - 1).G;
                        pixel_mask[3] = oldBitmap.GetPixel(x + 1, y).G;
                        pixel_mask[4] = oldBitmap.GetPixel(x, y).G;
                        pixel_mask[5] = oldBitmap.GetPixel(x + 1, y).G;
                        pixel_mask[6] = oldBitmap.GetPixel(x - 1, y + 1).G;
                        pixel_mask[7] = oldBitmap.GetPixel(x, y + 1).G;
                        pixel_mask[8] = oldBitmap.GetPixel(x + 1, y + 1).G;
                        pixS = 0;
                        for (int i = 0; i < 9; i++)
                            pixS += (pixel_mask[i] * Smoothing[i]);
                        pixS /= 9;
                        newBitmap.SetPixel(x, y, Color.FromArgb(pixS, pixS, pixS));
                    }
                this.pictureBox2.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newBitmap = new Bitmap(Width, Height);
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;

                // 用來存儲周圍像素的列表
                List<int> pixelValues = new List<int>();

                for (int x = 1; x < Width - 1; x++)
                {
                    for (int y = 1; y < Height - 1; y++)
                    {
                        pixelValues.Clear();

                        // 收集 3x3 區域的像素值
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                Color pixel = oldBitmap.GetPixel(x + dx, y + dy);
                                pixelValues.Add(pixel.G); // 使用 G 通道
                            }
                        }

                        // 排序並找到中位數
                        pixelValues.Sort();
                        int median = pixelValues[pixelValues.Count / 2];

                        // 設置新圖像的像素值
                        newBitmap.SetPixel(x, y, Color.FromArgb(median, median, median));
                    }
                }

                this.pictureBox2.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }
        }
    }
}