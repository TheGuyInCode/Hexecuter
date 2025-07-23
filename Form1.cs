using Hexecuter.Entities;
using Hexecuter.Enums;
using Hexecuter.Services.AbstractServices;
using Microsoft.VisualBasic.Logging;
using System.Threading.Tasks;

namespace Hexecuter
{
    public partial class Form1 : Form
    {
        private readonly IDeviceService _deviceService;
        private readonly IFirmwareService _firmwareService;
        private readonly IProgrammingLogService _programmingLogService;
        private Hexecuter.Entities.Device? selectedDevice;
        private ProgrammingLog? selectedProgrammingLog;
        private Firmware? _currentFirmware;
        private string? selectedImgFilePath;

        public Form1(IDeviceService deviceService, IFirmwareService firmwareService, IProgrammingLogService programmingLogService)
        {

            InitializeComponent();
            AddAboutButton();
            //selectedProgrammingLog = new ProgrammingLog();
            //selectedDevice = new Hexecuter.Entities.Device();
            //this.Load += new System.EventHandler(this.Form1_Load);
            dgvSDCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            _deviceService = deviceService;
            _firmwareService = firmwareService;
            _programmingLogService = programmingLogService;

            //this.Load += new System.EventHandler(this.Form1_Load);

            btnLoad.Enabled = false;

        }
        private async Task GetAllSdCards()
        {
            var getAllSdCards = await _deviceService.GetRemovableDrivesAsync();




            dgvSDCard.AutoGenerateColumns = true;
            dgvSDCard.DataSource = getAllSdCards.ToList();

        }
        private async Task GetAllConnectedDevices()
        {


            //var getAllDevices = (await _deviceService.GetConnectedDevicesAsync()).ToList();
            var getAllDevices = (await _deviceService.GetConnectedDevicesAsync())
                .Select(d => new Hexecuter.Entities.Device
                {
                    Id = d.Id,
                    DeviceName = d.DeviceName,
                    //Category = d.Category,
                    UsbIdentifier = d.UsbIdentifier,
                    RootPath = d.RootPath
                }).ToList();

            //dgvDevices.AutoGenerateColumns = true;
            dgvDevices.DataSource = getAllDevices;

        }
        private async Task GetAllProgrammingLogs()
        {

            var logs = (await _programmingLogService.GetAllAsync()).ToList();

            dgvLogs.AutoGenerateColumns = true;
            dgvLogs.DataSource = logs;

        }
        private void AddAboutButton()
        {
            var infoBtn = new Button
            {
                Text = "ⓘ",
                Width = 22,
                Height = 22,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.Transparent,
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 40, this.ClientSize.Height - 35)
                
            };

            infoBtn.FlatAppearance.BorderSize = 0;

            
            var tooltip = new ToolTip();
            tooltip.SetToolTip(infoBtn, "Hakkında");

           
            infoBtn.Click += (s, e) =>
            {
                using var about = new AboutForm();
                about.ShowDialog();
            };

            this.Controls.Add(infoBtn);
            infoBtn.BringToFront(); 
        }
        private async void btnBrowse_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Hex Files (*.hex)|*.hex",
                Title = "Lütfen bir .hex firmware dosyası seçin"
            };

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            if (string.IsNullOrWhiteSpace(dlg.FileName))
            {
                MessageBox.Show("Geçerli bir dosya seçmelisiniz!", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //_currentFirmware = await _firmwareService.LoadFromFileAsync(dlg.FileName);
                //txtFilePath.Text = _currentFirmware.FilePath;
                //btnLoad.Enabled = selectedDevice != null;
            }
            catch (FileNotFoundException fnf)
            {
                MessageBox.Show($"Dosya bulunamadı:\n{fnf.FileName}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu:\n{ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dgvDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //selectedDevice = (Hexecuter.Entities.Device)dgvDevices.SelectedRows[0].DataBoundItem;
        }


        private void dgvLogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //selectedProgrammingLog = (ProgrammingLog)dgvDevices.SelectedRows[0].DataBoundItem;
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDevices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDevices.SelectedRows.Count == 0)
            {
                selectedDevice = null!;
                btnLoad.Enabled = false;
                return;
            }

            selectedDevice = dgvDevices.SelectedRows[0].DataBoundItem as Hexecuter.Entities.Device;
            btnLoad.Enabled = (_currentFirmware != null);
        }

        private void dgvDevices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;
            //selectedDevice = dgvDevices.Rows[e.RowIndex].DataBoundItem as Hexecuter.Entities.Device;

            //btnLoad.Enabled = (_currentFirmware != null);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {



            await GetAllConnectedDevices();
            await GetAllProgrammingLogs();

            if (dgvLogs.Columns.Contains("Device") && dgvLogs.Columns.Contains("UpdatedDate") && dgvLogs.Columns.Contains("LogOutput") && dgvLogs.Columns.Contains("Id"))
            {
                dgvLogs.Columns["Device"].Visible = false;
                dgvLogs.Columns["UpdatedDate"].Visible = false;
                dgvLogs.Columns["LogOutput"].Visible = false;
                dgvLogs.Columns["Id"].Visible = false;
            }

            await GetAllSdCards();

            if (dgvSDCard.Columns.Contains("SerialPortName") && dgvSDCard.Columns.Contains("UpdatedDate"))
            {
                dgvSDCard.Columns["SerialPortName"].Visible = false;
                dgvSDCard.Columns["UpdatedDate"].Visible = false;
            }



            btnLoad.Enabled = false;
        }

        private void dgvLogs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLogs.SelectedRows.Count == 0)
            {
                selectedProgrammingLog = null!;
                btnLoad.Enabled = false;
                return;
            }

            selectedProgrammingLog = dgvLogs.SelectedRows[0].DataBoundItem as ProgrammingLog;
            btnLoad.Enabled = (_currentFirmware != null);
        }

        private void dgvSDCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLogs_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSDCard_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSDCard_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSDCard.SelectedRows.Count == 0)
            {
                selectedDevice = null;
                LoadSdCardBtn.Enabled = false;
                return;
            }

            selectedDevice = dgvSDCard.SelectedRows[0].DataBoundItem as Hexecuter.Entities.Device;

            choosenLbl.Text = selectedDevice.DeviceName + " - " + selectedDevice.UsbIdentifier;
            // Buton, hem cihaz hem firmware seçilirse enable olsun
            LoadSdCardBtn.Enabled = (selectedDevice != null);

            // veya

        }

        private async void browseSdCardBtn_Click(object sender, EventArgs e)
        {

            using var dlg = new OpenFileDialog
            {
                Filter = "IMG Files (*.img)|*.img",
                Title = "Lütfen bir .img firmware dosyası seçin"
            };

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            selectedImgFilePath = dlg.FileName;

            pathFileSdCardTxt.Text = selectedImgFilePath;

            await _firmwareService.LoadFromFileSdCardAsync(selectedImgFilePath);

            LoadSdCardBtn.Enabled = !string.IsNullOrWhiteSpace(selectedImgFilePath) && selectedDevice != null;

        }
        private async void LoadSdCardBtn_Click(object sender, EventArgs e)
        {

            progressBar1.Value = 0;
            progressBar1.Visible = true;
            progressBar1.Maximum = 100;

            var prog = new Progress<int>(percent =>
            {
                progressBar1.Value = percent;
                lblProgress.Text = $"{percent}%";
            });

            if (selectedDevice == null)
            {
                MessageBox.Show("Lütfen önce bir SD kart seçin");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedImgFilePath) || !File.Exists(selectedImgFilePath))
            {
                MessageBox.Show("Lütfen geçerli bir .img dosyası seçin");
                return;
            }


            try
            {

                var imgSize = new FileInfo(selectedImgFilePath).Length;
                var driveLetter = selectedDevice.RootPath?.Substring(0, 2);

                if (string.IsNullOrWhiteSpace(driveLetter) || !driveLetter.EndsWith(":"))
                {
                    MessageBox.Show("SD kartın sürücü harfi belirlenemedi.");
                    return;
                }

                var driveInfo = new DriveInfo(driveLetter);

                if (!driveInfo.IsReady)
                {
                    MessageBox.Show("SD kart hazır değil veya erişilemiyor.");
                    return;
                }

                if (imgSize > driveInfo.TotalSize)
                {
                    MessageBox.Show(".img dosyası SD karttan daha büyük! Yazma işlemi iptal edildi.");
                    return;
                }

                var confirm = MessageBox.Show(
                "Bu işlem SD kartın tüm içeriğini silecektir. Devam etmek istiyor musunuz?",
                "Uyarı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                var log = await Task.Run(() =>
                                            _programmingLogService.CopyFirmwareToSdCardAsync(selectedDevice, selectedImgFilePath, prog));

                MessageBox.Show(log.Message, log.IsSuccess ? "Başarılı" : "Hata",
                    MessageBoxButtons.OK, log.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                await GetAllProgrammingLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SD karta yazılırken hata oluştu:\n{ex.Message}");
            }
            finally
            {
                progressBar1.Visible = false;
                progressBar1.Value = 0;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pathFileSdCardTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click_1(object sender, EventArgs e)
        {

        }

        private async void extractToSDBtn_Click(object sender, EventArgs e)
        {

            progressBar1.Value = 0;
            progressBar1.Visible = true;
            progressBar1.Maximum = 100;

            var prog = new Progress<int>(percent =>
            {
                progressBar1.Value = percent;
                lblProgress.Text = $"{percent}%";
            });

            if (selectedDevice == null)
            {
                MessageBox.Show("Lütfen önce bir SD kart seçin");
                return;
            }
            if (string.IsNullOrWhiteSpace(selectedImgFilePath))
            {
                MessageBox.Show("Lütfen önce bir .img dosyası seçin");
                return;
            }

            try
            {

                var log = await _programmingLogService.ExtractImgToSdCardAsync(selectedDevice, selectedImgFilePath, prog);

                MessageBox.Show(log.Message, log.IsSuccess ? "Başarılı" : "Hata",
                    MessageBoxButtons.OK, log.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                await GetAllProgrammingLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SD karta yazılırken hata oluştu:\n{ex.Message}");
            }
            finally
            {
                progressBar1.Visible = false;
                progressBar1.Value = 0;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {

        }

        private void choosenLbl_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
