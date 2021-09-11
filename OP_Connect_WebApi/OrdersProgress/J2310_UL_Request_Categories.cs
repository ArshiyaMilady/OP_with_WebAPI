using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// هر سطح کاربری درخواست کدام دسته از کالاها را می تواند از انبار داشته باشد
namespace OrdersProgress
{
    public partial class J2310_UL_Request_Categories : X210_ExampleForm_Normal
    {
        long ul_id;
        List<Models.Category> lstCats = new List<Models.Category>();
        List<Models.User_Level> lstULs = new List<Models.User_Level>();
        List<Models.UL_Request_Category> lstUL_CRs = new List<Models.UL_Request_Category>();

        public J2310_UL_Request_Categories(long _ul_id)
        {
            InitializeComponent();
            ul_id = _ul_id;
            Text = "";
        }

        private async void J2310_UL_Request_Categories_Shown(object sender, EventArgs e)
        {
            if (Stack.Use_Web)
            {
                Models.User_Level ul = await HttpClientExtensions.GetT<Models.User_Level>
                    (Stack.API_Uri_start_read + "/User_Levels/" + ul_id);
                if (ul != null) Text = "   " + ul.Description;

                await GetData_web();
            }
            else
            {
                Text = "    " + Program.dbOperations.GetUser_LevelAsync(ul_id).Description;
                //dgvCats.DataSource = GetData();
                GetData();
            }
            ShowData();
        }

        private void GetData()
        {
            lstCats = Program.dbOperations.GetAllCategoriesAsync(Stack.Company_Id);
            lstULs= Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Id)
                .Where(d => d.Id != ul_id).ToList();
            lstUL_CRs = Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Id, ul_id);

            dgvCats.DataSource = lstCats;
            if(dgvCats.Rows.Count > 0) dgvCats.CurrentCell = dgvCats["Name", 0];

            dgvULs.DataSource = lstULs;
            if (dgvULs.Rows.Count > 0) dgvULs.CurrentCell = dgvULs["Description", 0];

            foreach (Models.UL_Request_Category ulrc in lstUL_CRs)
            {
                PutData_in_dgvUL_RC(ulrc);
            }
        }

        private async Task<bool> GetData_web()
        {
            lstCats = await HttpClientExtensions.GetT<List<Models.Category>>
                (Stack.API_Uri_start_read + "/Categories?all=no&company_Id=" + Stack.Company_Id, Stack.token);
            lstULs= await HttpClientExtensions.GetT<List<Models.User_Level>>
                (Stack.API_Uri_start_read + "/User_Levels?all=no&company_Id=" + Stack.Company_Id, Stack.token);
            lstULs = lstULs.Where(d => d.Id != ul_id).ToList();

            lstUL_CRs = await HttpClientExtensions.GetT<List<Models.UL_Request_Category>>
                (Stack.API_Uri_start_read + "/UL_Request_Category?all=no&company_Id=" + Stack.Company_Id, Stack.token);
            lstUL_CRs = lstUL_CRs.Where(d => d.User_Level_Id == ul_id).ToList();
            //Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Id, ul_id);

            dgvCats.DataSource = lstCats;
            if(dgvCats.Rows.Count > 0) dgvCats.CurrentCell = dgvCats["Name", 0];

            dgvULs.DataSource = lstULs;
            if (dgvULs.Rows.Count > 0) dgvULs.CurrentCell = dgvULs["Description", 0];

            foreach (Models.UL_Request_Category ulrc in lstUL_CRs)
            {
                PutData_in_dgvUL_RC(ulrc);
            }

            return true;
        }


        private void PutData_in_dgvUL_RC(Models.UL_Request_Category ul_rc)
        {
            int iRow = dgvUL_RC.Rows.Add();
            DataGridViewRow row = dgvUL_RC.Rows[iRow];
            row.Tag = ul_rc;
            row.Cells["colCategory_Name"].Value =lstCats.First(d=>d.Id==ul_rc.Category_Id).Name;
            if(ul_rc.Supervisor_UL_Id>0)
                row.Cells["colSupervisor_UL_Description"].Value = lstULs.First(d=>d.Id==ul_rc.Supervisor_UL_Id).Description;
        }

        private void ShowData()
        {
            #region جدول دسته ها
            foreach (DataGridViewColumn col in dgvCats.Columns)
            {
                switch (col.Name)
                {
                    //case "C_B1":
                    //    col.HeaderText = "انتخاب";
                    //    col.Width = 50;
                    //    break;
                    case "Name":
                        col.HeaderText = "نام دسته";
                        col.ReadOnly = true;
                        col.Width = 200;
                        break;

                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion

            #region جدول سطوح کاربری

            foreach (DataGridViewColumn col in dgvULs.Columns)
            {
                switch (col.Name)
                {
                    //case "C_B1":
                    //    col.HeaderText = "انتخاب";
                    //    col.Width = 50;
                    //    break;
                    case "Description":
                        col.HeaderText = "سطح کاربری سرپرست";
                        col.ReadOnly = true;
                        col.Width = 200;
                        break;
                    default: col.Visible = false; break;
                }

                if (col.ReadOnly) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else col.DefaultCellStyle.BackColor = Color.White;
            }
            #endregion

        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت تغییرات اطمینان دارید؟"
                , "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            panel1.Enabled = false;
            Application.DoEvents();

            if (Stack.Use_Web) await SaveData_web();
            else SaveData();

            MessageBox.Show("تغییرات با موفقیت ثبت گردید.");
            Close();
        }

        private void SaveData()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Id, ul_id).Any())
            {
                foreach (Models.UL_Request_Category ul_request_category
                    in Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Id, ul_id))
                {
                    if (!lstCats.Where(d => d.C_B1).Any(d => d.Id == ul_request_category.User_Level_Id))
                    {
                        Program.dbOperations.DeleteUL_Request_CategoryAsync(ul_request_category);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Category cat in lstCats.Where(d => d.C_B1).ToList())
            {
                if (!Program.dbOperations.GetAllUL_Request_CategoriesAsync(Stack.Company_Id, ul_id)
                    .Any(d => d.Category_Id == cat.Id))
                {
                    // تأیید مدیریت ، ابتدا نیاز به تأیید سرپرست دارد
                    if (cat.Need_Manager_Confirmation) cat.Need_Supervisor_Confirmation = true;
                    Program.dbOperations.AddUL_Request_Category(new Models.UL_Request_Category
                    {
                        Company_Id = Stack.Company_Id,
                        User_Level_Id = ul_id,
                        Category_Id = cat.Id,
                        //Need_Supervisor_Confirmation = cat.Need_Supervisor_Confirmation,
                        //Need_Manager_Confirmation = cat.Need_Manager_Confirmation,
                    });
                }
            }
            #endregion


        }

        private async Task<bool> SaveData_web()
        {
            #region حذف شود : قبلا بوده است، اما در جدول انتخاب نشده است 
            if (lstUL_CRs.Any())
            {
                foreach (Models.UL_Request_Category ul_request_category in lstUL_CRs)
                {
                    if (!lstCats.Where(d => d.C_B1).Any(d => d.Id == ul_request_category.User_Level_Id))
                    {
                        await HttpClientExtensions.DeleteAsJsonAsync2
                           (Stack.API_Uri_start + "/UL_Request_Category/" + ul_request_category.Id, Stack.token);
                    }
                }
            }
            #endregion

            #region موارد جدید اضافه شود 
            foreach (Models.Category cat in lstCats.Where(d => d.C_B1).ToList())
            {
                if (!lstUL_CRs.Any(d => d.Category_Id == cat.Id))
                {
                    // تأیید مدیریت ، ابتدا نیاز به تأیید سرپرست دارد
                    if (cat.Need_Manager_Confirmation) cat.Need_Supervisor_Confirmation = true;
                    Models.UL_Request_Category ulrc = new Models.UL_Request_Category
                    {
                        Company_Id = Stack.Company_Id,
                        User_Level_Id = ul_id,
                        Category_Id = cat.Id,
                        //Need_Supervisor_Confirmation = cat.Need_Supervisor_Confirmation,
                        //Need_Manager_Confirmation = cat.Need_Manager_Confirmation,
                    };
                    await HttpClientExtensions.PostAsJsonAsync(Stack.API_Uri_start_read
                      + "/UL_Request_Category", ulrc, Stack.token);
                }
            }
            #endregion

            return true;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("این امکان فعال نمی باشد");
            return;

        }

        bool bChooseAll = false;
        private void BtnChooseAll_Click(object sender, EventArgs e)
        {
            bChooseAll = !bChooseAll;

            foreach (DataGridViewRow row in dgvCats.Rows.Cast<DataGridViewRow>())
                row.Cells["C_B1"].Value = bChooseAll;
            if (bChooseAll) btnChooseAll.Text = "عدم انتخاب همه";
            else btnChooseAll.Text = "انتخاب همه";
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //dgvULs.Enabled = !checkBox1.Checked;
        }

        private async void BtnAddNew_Click(object sender, EventArgs e)
        {
            //try
            {
                long cat_index = Convert.ToInt64(dgvCats.CurrentRow.Cells["Id"].Value);
                long supervisor_ul_index = checkBox1.Checked ? 0 : Convert.ToInt64(dgvULs.CurrentRow.Cells["Id"].Value);
                if (lstUL_CRs.Any(d => (d.Category_Id==cat_index)
                   && (d.Supervisor_UL_Id == supervisor_ul_index)))
                {
                    MessageBox.Show("این مورد قبلا ثبت شده است","خطا");
                    return;
                }

                //long ul_cr_id = 0;

                Models.UL_Request_Category ulrc = new Models.UL_Request_Category
                {
                    Company_Id = Stack.Company_Id,
                    User_Level_Id = ul_id,
                    Category_Id = cat_index,
                    Supervisor_UL_Id = supervisor_ul_index,
                };

                if (Stack.Use_Web)
                {
                    var response = await HttpClientExtensions.PostAsJsonAsync
                        (Stack.API_Uri_start_read + "/UL_Request_Category", ulrc, Stack.token);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("اشکال در ثبت اطلاعات", "خطا");
                        return;
                    }
                    else
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        Models.UL_Request_Category ulrc1 = JsonConvert.DeserializeObject<Models.UL_Request_Category>(responseString);
                        PutData_in_dgvUL_RC(ulrc1);
                    }
                }
                else
                {
                    long ul_cr_id = Program.dbOperations.AddUL_Request_Category(ulrc);
                    PutData_in_dgvUL_RC(Program.dbOperations.GetUL_Request_CategoryAsync(ul_cr_id));
                }
            }
            //catch { }
        }

        private async void DgvUL_RC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if(dgvUL_RC.Columns[e.ColumnIndex].Name.Equals("colRemove"))
            {
                if (MessageBox.Show("آیا از حذف مورد انتخاب شده ، اطمینان دارید؟"
                    , "اخطار", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                DataGridViewRow row = dgvUL_RC.Rows[e.RowIndex];
                Models.UL_Request_Category ulrc = (Models.UL_Request_Category)row.Tag;

                if (Stack.Use_Web)
                    await HttpClientExtensions.DeleteAsJsonAsync2(Stack.API_Uri_start_read
                        + "/UL_Request_Category/" + ulrc.Id, Stack.token);
                else Program.dbOperations.DeleteUL_Request_CategoryAsync(ulrc);

                dgvUL_RC.CurrentCell = null;
                dgvUL_RC.Rows.Remove(row);
            }
        }






        //
    }
}
