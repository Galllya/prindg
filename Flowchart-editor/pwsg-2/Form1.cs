using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;

namespace pwsg_2
{
    public partial class Form1 : Form
    {
        List<Block> blocks = new List<Block>();
        Block currentBlock;
        Block selectedBlock;
        bool moving=false; int rel_x, rel_y;
        bool trash_mode=false;
        Graphics flagGraphics;
        Pen linePen=new Pen(Color.Black,1);

        //задаем уже достигнутую высоту, в начале (после отрисовки НАЧАЛО и СТРЕЛКА) она равна 150
        int last_height = 150;

        //задаем ширину, с которой работаем

        int last_weidth = 300;

        //место в списке выбранной стрелки
        int select_arrow_index = 1;

        //если выбранная стрелка - часть фигуры выбора
    
        public Form1()
        {
            InitializeComponent();
            //изначально кнопки для выбора фигур не видны
            changeVisible(false);


        }

        private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyResources(resources, ctl.Controls);
            }
        }

        private void PrintAll(bool refresh = true)
        {
            foreach(Block b in blocks)
            {
                b.Print(false);
            }
            if (refresh)
            {
                flagGraphics.DrawImage(pictureBox1.Image, 0, 0);
                pictureBox1.Refresh();
            }
        }
        private void ReprintPicture(bool refresh=true)
        {
            flagGraphics.FillRectangle(Brushes.White,0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);
            PrintAll(refresh);
        }


        private  Block NearestBlock(int x,int y)
        {
            if (blocks.Count == 0) return null;
            Block nearest = new OperationBlock();
            nearest.SetCoordinates(int.MaxValue, int.MaxValue);

            foreach (Block b in blocks)
                if (b.IsInside(x, y))
                    if ((x - nearest.x) * (x - nearest.x) + (y - nearest.y) * (y - nearest.y) > (x - b.x) * (x - b.x) + (y - b.y) * (y - b.y))
                        nearest = b;
            if (nearest.x == int.MaxValue)
                return null;
            return nearest;
        }

    
        public void CreateClearBitmap(int width, int height)
        {


            pictureBox1.Image = new Bitmap(width, height);
            Graphics flagGraphics = Graphics.FromImage(pictureBox1.Image);
            this.flagGraphics = flagGraphics;
            flagGraphics.FillRectangle(Brushes.White, 0, 0, width, height);
            flagGraphics.DrawImage(pictureBox1.Image, width, height);
            pictureBox1.Refresh();
    
            foreach (Block b in blocks)
                b.Clear();
            blocks.Clear();
        }
        public void CreateBitmapAtRuntime()
        {
            pictureBox1.Size = new Size(210, 110);
            this.Controls.Add(pictureBox1);

            Bitmap flag = new Bitmap(200, 100);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int red = 0;
            int white = 11;
            while (white <= 100)
            {
                flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
                flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
                red += 20;
                white += 20;
            }
            pictureBox1.Image = flag;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            CreateClearBitmap(2000, 2000);
            trash_mode = false;
            currentBlock = new StartBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            if (blok != null)
            {
                blok.SetCoordinates(300, 50);
                blok.Print();
                blocks.Add(blok);
            }
            currentBlock = new Arrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok1 = currentBlock.CloneNew();
            if (blok1 != null)
            {
                blok1.SetCoordinates(348, 100);
                blok1.Print();
                blocks.Add(blok1);
            }


        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moving)
                if (e.Button == MouseButtons.Middle)
                { 
                    if (selectedBlock != null)
                    {
                        moving = true;
                        rel_x = selectedBlock.x - e.X;
                        rel_y = selectedBlock.y - e.Y;
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {

                    //если нажали кнопку удалить - происходит удаление зажатой фигуры
                    if(trash_mode)
                    {
                        Block blok = NearestBlock(e.X, e.Y);
                        Block select_blok = blok;
                        if (blok != null)
                        {
                            last_height = blok.y;
                            //тут находим фигуру, которая идет за выбранным блоком на удаление, у нас это всегда стрелка - удаляем ее 
                            Block next_arrow = blocks.Where(bloc => bloc.y > blok.y).First();

                            blok.Clear();
                            blocks.Remove(blok);

                        


                            if (select_blok.text == "ЛОГИЧЕСКОЕ УСЛОВИЕ")
                            {
                                foreach (Block b in blocks.Where(bloc => bloc.y > blok.y).Take(8).ToArray())
                                {

                                    b.Clear();
                                    blocks.Remove(b);

                                }


                            }

                            if (select_blok.text == "ЦИКЛ")
                            {
                                foreach (Block b in blocks.Where(bloc => bloc.y > blok.y).Take(10).ToArray())
                                {

                                    b.Clear();
                                    blocks.Remove(b);

                                }


                            }
                            else
                            {
                                //тут просто вылазиет странная ошибка, хз как по человески менять
                                if (blocks.Count != 3)
                                {
                                    next_arrow.Clear();
                                    blocks.Remove(next_arrow);
                                }

                            }
                            //тут перерисовываем все фигуры, которые были снизу
                            if (select_blok.text == "ЛОГИЧЕСКОЕ УСЛОВИЕ")
                                ReprintButtonFigures(150, false);
                            else
                            {
                                if (select_blok.text == "ЦИКЛ")
                                    ReprintButtonFigures(330, false);
                                 else
                                ReprintButtonFigures(98, false);
                            }



                            trash_mode = false;
                            if (blok == selectedBlock)
                                selectedBlock = null;
                        }
                        label1.Text = "Нажмите на СТРЕЛКУ, чтобы открыть меню выбора фигур";

                    }
                    else
              // проверяем, если фигура стрелка - показываем кнопки для выбора остальных фигур
                    {
                        Block nearBlok = NearestBlock(e.X, e.Y);
                        if (nearBlok != null)

                        {
                            if (nearBlok.text == "СТРЕЛКА")
                            {
                                label1.Text = "Выберите ФИГУРУ которая вам необходима";
                                //в качестве последней высоты используем высоту выбранной стрелки
                                last_height = nearBlok.y + 50;
                                last_weidth = nearBlok.x-50;

                                select_arrow_index = blocks.IndexOf(nearBlok);
                                changeVisible(true);
                            }


                 

                        }

                        

                    }



                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (selectedBlock != null)
                    {
                        selectedBlock.drawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        textBox1.Enabled = false;
                        textBox1.Text = string.Empty;
                    }
                    selectedBlock = NearestBlock(e.X, e.Y);
                    if (selectedBlock != null)
                    {
                        selectedBlock.drawPen.DashPattern = new float[] { 2.0F, 2.0F };
                        if (selectedBlock.text != "НАЧАЛО")
                            textBox1.Enabled = true;
                        textBox1.Text = selectedBlock.text;
                    }
                    ReprintPicture();
                }
        }

     

       //функция для изменения видимости кнопок выбора фигур
       private void changeVisible(bool isVisible)
        {
            if (isVisible)
            {
                InputBlockButton.Visible = true;
                operationBlockButton.Visible = true;
                decisionBlockButton.Visible = true;
                CycleBlockButton.Visible = true;
                stopBlockButton.Visible = true;
            }
            else
            {
                InputBlockButton.Visible = false;
                operationBlockButton.Visible = false;
                decisionBlockButton.Visible = false;
                CycleBlockButton.Visible = false;
                stopBlockButton.Visible = false;
            }
        }
        private void decisionBlockButton_Click(object sender, EventArgs e)
        {
            ReprintButtonFigures(180, true);
            trash_mode = false;
               decisionBlockButton.BackColor = Color.FromArgb(192, 192, 255);
               decisionBlockButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
               currentBlock = new DecisionBlock();
               currentBlock.SetGraphics(pictureBox1, flagGraphics);
               Block blok = currentBlock.CloneNew();
               if (blok != null)
               {
                   blok.SetCoordinates(last_weidth, last_height);
                   blok.Print();
                    blocks.Insert(select_arrow_index + 1, blok);
               }
            int height_for_arrow = last_height + 49;

            //рисуем правую часть стрелки
            currentBlock = new HorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok1 = currentBlock.CloneNew();
            if (blok1 != null)
            {
                blok1.SetCoordinates(last_weidth+125, height_for_arrow);
                blok1.Print();
                blocks.Insert(select_arrow_index + 2, blok1);
        

            }

            //рисуем левую часть стрелки

            currentBlock = new HorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok2 = currentBlock.CloneNew();
            if (blok2 != null)
            {
                blok2.SetCoordinates(last_weidth - 125, height_for_arrow);
                blok2.Print();
                blocks.Insert(select_arrow_index + 3, blok2);


            }
            currentBlock = new LongVerticalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok4 = currentBlock.CloneNew();
            if (blok4 != null)
            {
                blok4.SetCoordinates(last_weidth - 125+25, height_for_arrow-22);
                blok4.Print();
                blocks.Insert(select_arrow_index + 4, blok4);

            }
            currentBlock = new LongVerticalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok5 = currentBlock.CloneNew();
            if (blok5 != null)
            {
                blok5.SetCoordinates(last_weidth + 199, height_for_arrow - 22);
                blok5.Print();
                blocks.Insert(select_arrow_index + 5, blok5);

            }

            currentBlock = new OperationBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok8 = currentBlock.CloneNew();
            if (blok8 != null)
            {
                blok8.SetCoordinates(last_weidth - 150, height_for_arrow );
                blok8.Print();
                blocks.Insert(select_arrow_index + 6, blok8);

            }

            currentBlock = new OperationBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok9 = currentBlock.CloneNew();
            if (blok9 != null)
            {
                blok9.SetCoordinates(last_weidth + 150, height_for_arrow);
                blok9.Print();
                blocks.Insert(select_arrow_index + 7, blok9);

            }


            currentBlock = new LongHorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok6 = currentBlock.CloneNew();
            if (blok6 != null)
            {
                blok6.SetCoordinates(last_weidth - 125, height_for_arrow +105);
                blok6.Print();
                blocks.Insert(select_arrow_index + 8, blok6);

            }

            currentBlock = new Arrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok7 = currentBlock.CloneNew();
            if (blok7 != null)
            {
                blok7.SetCoordinates(last_weidth+48, height_for_arrow + 80);
                blok7.Print();
                blocks.Insert(select_arrow_index + 9, blok7);

            }


            changeVisible(false);
            label1.Text = "Нажмите на СТРЕЛКУ, чтобы открыть меню выбора фигур";

        }


        private void operationBlockButton_Click(object sender, EventArgs e)
        {
            ReprintButtonFigures(98, true);

            trash_mode = false;
            operationBlockButton.BackColor = Color.FromArgb(192, 192, 255);
            operationBlockButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
            currentBlock = new OperationBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            if (blok != null)
            {
                blok.SetCoordinates(last_weidth, last_height);
                blok.Print();
                blocks.Insert(select_arrow_index + 1, blok);
            }

            int height_for_arrow = last_height + 50;

            DrawArrow(height_for_arrow);
            changeVisible(false);
            label1.Text = "Нажмите на СТРЕЛКУ, чтобы открыть меню выбора фигур";


        }


        //функция для перерисовки фигур, которые оказались ниже новых - добавленных/удаленных
        private void ReprintButtonFigures(int new_figure_height, bool isAdd)
        {
            if (blocks.Count != 0)
                foreach (Block b in blocks.ToArray()) {
                    if (b.y >= last_height)
                    {
                        currentBlock = b;
                        currentBlock.SetGraphics(pictureBox1, flagGraphics);
                        Block blok1 = currentBlock.CloneNew();
                        if (blok1 != null)
                        {
                            if (isAdd)
                                blok1.SetCoordinates(b.x, b.y + new_figure_height);
                            else blok1.SetCoordinates(b.x, b.y - new_figure_height);

                            blok1.Print();
                            blocks.Add(blok1);
                        }
                        b.Clear();
                        blocks.Remove(b);
                        ReprintPicture();
                    }
                }
              
        }
        private void InputBlockButton_Click(object sender, EventArgs e)
        {
            ReprintButtonFigures(100, true);


                trash_mode = false;
            InputBlockButton.BackColor = Color.FromArgb(192, 192, 255);
            InputBlockButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
            currentBlock = new InputBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            if (blok != null)
            {
                blok.SetCoordinates(last_weidth, last_height);
                blok.Print();
                //добавляем блок в список на КОНКРЕТНОЕ место - за быаранной стрелкой
                blocks.Insert(select_arrow_index + 1, blok);
            }

            //задаем высоту для прорисовки стрелки
            int height_for_arrow = last_height + 45;

            DrawArrow(height_for_arrow);

            //после отрисовки фигуры уберам кнопки выбора фигур
            changeVisible(false);
            label1.Text = "Нажмите на СТРЕЛКУ, чтобы открыть меню выбора фигур";

        }


        //функция для рисования стрелки
      private void DrawArrow (int height_for_arrow, int weidth_for_arrow = 348)
        {
            currentBlock = new Arrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok1 = currentBlock.CloneNew();
            if (blok1 != null)
            {
                blok1.SetCoordinates(last_weidth+48, height_for_arrow);
                blok1.Print();
                blocks.Insert(select_arrow_index+2, blok1);

            }
        }
        private void CycleBlockButton_Click(object sender, EventArgs e)
        {
            ReprintButtonFigures(380, true);

            trash_mode = false;
            CycleBlockButton.BackColor = Color.FromArgb(192, 192, 255);
            CycleBlockButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
            currentBlock = new CycleBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            if (blok != null)
            {
                blok.SetCoordinates(300, last_height);
                blok.Print();
                blocks.Add(blok);
            }

            int height_for_arrow = last_height + 50;


            currentBlock = new LongVerticalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok2 = currentBlock.CloneNew();
            if (blok2 != null)
            {
                blok2.SetCoordinates(last_weidth+48, height_for_arrow-5);
                blok2.Print();
                blocks.Insert(select_arrow_index + 2, blok2);


            }

            currentBlock = new OperationBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok3 = currentBlock.CloneNew();
            if (blok3 != null)
            {
                blok3.SetCoordinates(last_weidth , height_for_arrow +50);
                blok3.Print();
                blocks.Insert(select_arrow_index + 3, blok3);


            }

            currentBlock = new LongVerticalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok4 = currentBlock.CloneNew();
            if (blok4 != null)
            {
                blok4.SetCoordinates(last_weidth + 48, height_for_arrow +100);
                blok4.Print();
                blocks.Insert(select_arrow_index + 4, blok4);


            }

            currentBlock = new MiddleHorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok5 = currentBlock.CloneNew();
            if (blok5 != null)
            {
                blok5.SetCoordinates(last_weidth-74 , height_for_arrow + 225);
                blok5.Print();
                blocks.Insert(select_arrow_index + 5, blok5);


            }

            currentBlock = new MiddleVerticalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok6 = currentBlock.CloneNew();
            if (blok6 != null)
            {
                blok6.SetCoordinates(last_weidth - 74, height_for_arrow +8);
                blok6.Print();
                blocks.Insert(select_arrow_index + 6, blok6);


            }

            currentBlock = new HorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok7 = currentBlock.CloneNew();
            if (blok7 != null)
            {
                blok7.SetCoordinates(last_weidth - 74, height_for_arrow + 8);
                blok7.Print();
                blocks.Insert(select_arrow_index + 7, blok7);


            }

            currentBlock = new HorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok8 = currentBlock.CloneNew();
            if (blok8 != null)
            {
                blok8.SetCoordinates(last_weidth + 74, height_for_arrow + 8);
                blok8.Print();
                blocks.Insert(select_arrow_index + 8, blok8);


            }

            currentBlock = new VeryLongVerticalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok9 = currentBlock.CloneNew();
            if (blok9 != null)
            {
                blok9.SetCoordinates(last_weidth + 128, height_for_arrow + 8);
                blok9.Print();
                blocks.Insert(select_arrow_index + 9, blok9);

            }

            currentBlock = new LongMiddleHorizantalArrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok10 = currentBlock.CloneNew();
            if (blok10 != null)
            {
                blok10.SetCoordinates(last_weidth + 28, height_for_arrow + 310);
                blok10.Print();
                blocks.Insert(select_arrow_index + 10, blok10);

            }

            currentBlock = new Arrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok11 = currentBlock.CloneNew();
            if (blok11 != null)
            {
                blok11.SetCoordinates(last_weidth + 48, height_for_arrow + 285);
                blok11.Print();
                blocks.Insert(select_arrow_index + 11, blok11);

            }



            changeVisible(false);

        }




        private void trashButton_Click(object sender, EventArgs e)
        {
            trashButton.BackColor = Color.FromArgb(192, 192, 255);
            trashButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
            trash_mode = true;
            label1.Text = "Выберите ФИГУРУ, которую хотите удалить";

        }

        private void stopBlockButton_Click(object sender, EventArgs e)
        {
            trash_mode = false;
            stopBlockButton.BackColor = Color.FromArgb(192, 192, 255);
            stopBlockButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
            currentBlock = new StopBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            if (blok != null)
            {
                blok.SetCoordinates(last_weidth, last_height);
                blok.Print();
                blocks.Insert(select_arrow_index + 1, blok);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Enabled)
            {
                selectedBlock.text = textBox1.Text;
                ReprintPicture();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Diagram files (*.diag)|*.diag";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream file = fileDialog.OpenFile();

                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(file, blocks);
                file.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentBlock = new OperationBlock();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            blok.SetCoordinates(100, 100);
            blok.Print();
            blocks.Add(blok);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trash_mode = false;
            InputBlockButton.BackColor = Color.FromArgb(192, 192, 255);
            InputBlockButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255 / 3, 192, 192, 255);
            currentBlock = new Arrow();
            currentBlock.SetGraphics(pictureBox1, flagGraphics);
            Block blok = currentBlock.CloneNew();
            if (blok != null)
            {
                blok.SetCoordinates(300, 150);
                blok.Print();
                blocks.Add(blok);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Diagram files (*.diag)|*.diag";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                blocks = null;

                //Open the file written above and read values from it.
                Stream file = File.Open(fileDialog.FileName, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();

                blocks = (List<Block>)bformatter.Deserialize(file);
                file.Close();
                foreach (Block b in blocks)
                    b.SetGraphics(pictureBox1, flagGraphics);
                ReprintPicture();

            }
        } 

            private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(moving)
            {
                selectedBlock.SetCoordinates(e.X + rel_x, e.Y + rel_y);
                ReprintPicture();
            }
        }
    }
}
