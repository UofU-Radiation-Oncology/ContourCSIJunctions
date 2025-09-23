using System;
using System.Linq;
using System.Text;
using System.Windows;
using WinForms = System.Windows.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Image = VMS.TPS.Common.Model.API.Image;
using System.Windows.Media.Media3D;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0")]
[assembly: AssemblyInformationalVersion("1.0")]

// Script needs write access
[assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }
        // variable initialization
        public System.Windows.Media.Brush Foreground { get; set; }
        private Patient _patient;
        private Course _course;
        private StructureSet _ss;
        private PlanSetup _plan;
        private ListBox structureList;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext context, System.Windows.Window window/*, ScriptEnvironment environment*/)
        {
            // Validate patient in current context
            ValidatePatient(context);
            // Validate the course in current context
            ValidateCourse(context);
            // Validate structure set in current context
            ValidateStructureSet(context);
            // Validate plan in current context
            ValidatePlan(context);
            try
            {
                _patient = GetPatient(context);
                _ss = GetStructureSet(context);
                _course = GetCourse(context);
                _plan = GetPlan(context);

                var beamGroups = _plan.Beams.Where(b => !b.IsSetupField).GroupBy(b => new { b.IsocenterPosition.x, b.IsocenterPosition.y, b.IsocenterPosition.z }).ToList();

                ibn groupIdx = 1;
                var groupStructures

                MessageBox.Show(string.Format("You are working with patient {0}, course {1}, structure set {2} and plan {3}",_patient.Id, _course.Id,_structureSet.Id, _plan.Id));
                //#region Starting to generate the UI

                //// main container
                //StackPanel spMain = new StackPanel
                //{
                //    Orientation = Orientation.Vertical,
                //    HorizontalAlignment = HorizontalAlignment.Left,
                //    Margin = new Thickness(0, 10, 0, 0),
                //    Width = 600
                //};
                //// title
                //_TitleBlock = new TextBlock
                //{
                //    Text = "Structure Downsampler",
                //    FontSize = 32,
                //    FontWeight = FontWeights.Bold,
                //    HorizontalAlignment = HorizontalAlignment.Left,
                //    Margin = new Thickness(0, 0, 0, 0),
                //    Foreground = System.Windows.Media.Brushes.MediumBlue
                //};

                //// author info
                //_AuthorBlock = new TextBlock
                //{
                //    Text = "University of Utah Huntsman Cancer Institute (nicholas.nelson@hci.utah.edu)",
                //    FontSize = 14,
                //    HorizontalAlignment = HorizontalAlignment.Left,
                //    Margin = new Thickness(0, 0, 0, 0),
                //    TextWrapping = TextWrapping.Wrap
                //};

                //_notesBlock = new TextBlock
                //{
                //    Text = string.Format("This script will identify high resolution Eclipse contours and, if selected, convert them to default resolution. This" +
                //"is useful for optimizations on large datasets (such as VMAT CSI) that have a lot of high resolution structures (which often come from MIM Protege)"),
                //    HorizontalAlignment = HorizontalAlignment.Left,
                //    Margin = new Thickness(0, 5, 5, 0),
                //    TextWrapping = TextWrapping.Wrap,
                //    VerticalAlignment = VerticalAlignment.Bottom
                //};

                //// Patient info
                //_patientNameBlock = new TextBlock
                //{
                //    Text = "PATIENT INFO",
                //    FontSize = 28,
                //    FontWeight = FontWeights.Bold,
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //    Margin = new Thickness(0, 10, 0, 0)
                //};

                //_patientInfoBlock = new TextBlock
                //{
                //    Text = string.Format("Name: {0}\n" +
                //    "Structure Set ID: {1}\n", _patient.Name, _structureSet.Id),
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //    Margin = new Thickness(0, 0, 0, 0)
                //};

                //// table title
                //var tableTitleBlock = new TextBlock
                //{
                //    Text = "High resolution structures",
                //    FontSize = 24,
                //    FontWeight = FontWeights.Bold,
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //    Margin = new Thickness(0, 0, 0, 0)
                //};

                //structureList = new ListBox
                //{
                //    SelectionMode = SelectionMode.Multiple,
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //    Width = 350
                //};

                //foreach (var contour in _HiResContourList)
                //{
                //    structureList.Items.Add(new CheckBox
                //    {
                //        Content = string.Format("{0} (Volume = {1} cc)",contour.Id,Math.Round(contour.Volume, 2)),
                //        Tag = contour,
                //        IsChecked = false
                //    });
                //}

                //_checkedStructureBox = new WinForms.CheckedListBox
                //{
                //    Dock = WinForms.DockStyle.Fill,
                //    CheckOnClick = true // lets you click with single click
                //};

                //// button to calculate CBCT dose
                //var _convertStructuresButton = new Button
                //{

                //    // button content - what it says
                //    Content = "Convert selected structures to default resolution",

                //    // a little padding
                //    Padding = new Thickness(10),
                //    Cursor = Cursors.Hand,
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //    Width = 350,
                //    Margin = new Thickness(10, 10, 10, 10)
                //};

                //#endregion End of ComboBox/StackPanel setup

                //#region Calculation/replanning button clicking region
                //_convertStructuresButton.Click += _convertStructuresButton_Click;

                //#endregion End of Calculation region

                //#region Final UI presentation
                //// add to main stack panel
                //spMain.Children.Add(_TitleBlock);
                //spMain.Children.Add(_AuthorBlock);
                //spMain.Children.Add(_notesBlock);
                //spMain.Children.Add(_patientNameBlock);
                //spMain.Children.Add(_patientInfoBlock);
                //spMain.Children.Add(tableTitleBlock);
                //spMain.Children.Add(structureList);
                //spMain.Children.Add(_convertStructuresButton);
                //spMain.VerticalAlignment = VerticalAlignment.Stretch;



                //// window settings
                //window.Title = "Structure Downsampler";
                //window.FontFamily = new System.Windows.Media.FontFamily("Calibri");
                //window.FontSize = 14;
                //window.Width = spMain.Width + 50;
                //window.Height = spMain.Height + 50;
                //window.WindowStartupLocation = WindowStartupLocation.Manual;
                //window.Content = spMain;
                //#endregion End of final UI presentation

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Sorry, something went wrong.\n\n{0}\n\n{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }



        //HELPER FUNCTIONS

        /// <summary>
        /// Validates that the current context is a Patient
        /// <para></para>Will alert the user and end the script
        /// </summary>
        /// <param name="context"></param>
        private void ValidatePatient(ScriptContext context)
        {
            if (context.Patient == null)
            {
                MessageBox.Show("Please open a patient");
                return;
            }
        }

        /// <summary>
        /// Validates that the current context is a Course
        /// <para></para>Will alert the user and end the script
        /// </summary>
        /// <param name="context"></param>
        private void ValidateCourse(ScriptContext context)
        {
            if (context.Course == null)
            {
                MessageBox.Show("Please open a course");
                return;
            }
        }

        /// <summary>
        /// Validates that the current context is a Course
        /// <para></para>Will alert the user and end the script
        /// </summary>
        /// <param name="context"></param>
        private void ValidateStructureSet(ScriptContext context)
        {
            if (context.StructureSet == null)
            {
                MessageBox.Show("Please open a structure set");
                return;
            }
        }

        /// <summary>
        /// Validates that the current context is a plan
        /// <para></para>Will alert the user and end the script
        /// </summary>
        /// <param name="context"></param>
        private void ValidatePlan(ScriptContext context)
        {
            if (context.StructureSet == null)
            {
                MessageBox.Show("Please open a plan");
                return;
            }
        }

        /// <summary>
        /// Gets the image of the current plan (which should be the CT sim)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Common.Model.API.Image GetSimImage(ScriptContext context)
        {
            return context.Image;
        }

        private static IEnumerable<Beam> GetBeams(ScriptContext context)
        {
            return context.PlanSetup.Beams;
        }

        /// <summary>
        /// Returns the plan setup in the current context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static PlanSetup GetPlanSetup(ScriptContext context)
        {
            return context.PlanSetup;
        }

        private StructureSet GetStructureSet(ScriptContext context)
        {
            return context.StructureSet;
        }

        /// <summary>
        /// Gets plan
        /// </summary>
        /// <param name="context"></param>
        private PlanSetup GetPlan(ScriptContext context)
        {
            return context.PlanSetup;
        }

        /// <summary>
        /// Gets study
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IEnumerable<Study> GetStudy(ScriptContext context)
        {
            return context.Patient.Studies;
        }

        /// <summary>
        /// Gets course
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Course GetCourse(ScriptContext context)
        {
            return context.Course;
        }



        /// <summary>
        /// Validates that the current context is a Course
        /// <para></para>Will alert the user and end the script
        /// </summary>
        /// <param name="context"></param>
        //private void VerifyStructureTypes(StructureSet ssToCheck)
        //{
        //    foreach (Structure contour in ssToCheck)
        //    {
        //        if (contour.DicomType == "None")
        //        {
        //            contour.DicomType.
        //        }
        //    }
        //}

        /// <summary>
        /// Gets the patient in the current context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Patient GetPatient(ScriptContext context)
        {
            return context.Patient;
        }

        /// <summary>
        /// Template delegate used for progress reporting when the operations is being performed in a separate helper class
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="msg"></param>
        /// <param name="f"></param>
        public delegate void ProvideUIUpdateDelegate(int comp, string msg = "", bool f = false);

        /// <summary>
        /// Button that converts structures to default resolution.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _convertStructuresButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedContours = new List<Structure>();

            foreach (CheckBox cb in structureList.Items)
            {
                if (cb.IsChecked == true)
                {
                    if (cb.Tag is Structure contour)
                    {
                        selectedContours.Add(contour); // the tag is the contour 
                    }
                }
            }

            MessageBox.Show($"You selected {selectedContours.Count} contours for conversion");
            _patient.BeginModifications();
            foreach (Structure contour in selectedContours)
            {
                OverWriteHighResStructureWithLowResStructure(contour, _structureSet);
            }

            MessageBox.Show("Successfully converted the contours! You can now close out of the window.");
        }

        /// <summary>
        /// Method to take a high resolution structure as input and overwrite it with a new structure that is default resolution
        /// </summary>
        /// <param name="theStructure"></param>
        /// <returns></returns>
        private static bool OverWriteHighResStructureWithLowResStructure(Structure theStructure, StructureSet selectedSS)
        {
            //ProvideUIUpdate(0, $"Retrieving all contour points for: {theStructure.Id}");
            int startSlice = CalculationHelper.ComputeSlice(theStructure.MeshGeometry.Positions.Min(p => p.Z), selectedSS);
            int stopSlice = CalculationHelper.ComputeSlice(theStructure.MeshGeometry.Positions.Max(p => p.Z), selectedSS);
            //ProvideUIUpdate(0, $"Start slice: {startSlice}");
            //ProvideUIUpdate(0, $"Stop slice: {stopSlice}");
            VVector[][][] structurePoints = GetAllContourPoints(theStructure, startSlice, stopSlice);///, ProvideUIUpdate);
            //ProvideUIUpdate(0, $"Contour points for: {theStructure.Id} loaded");

            //ProvideUIUpdate(0, $"Removing and re-adding {theStructure.Id} to structure set");
            (bool fail, Structure lowResStructure) = RemoveAndReAddStructure(theStructure, selectedSS);///, ProvideUIUpdate);
            if (fail) return true;

            //ProvideUIUpdate(0, $"Contouring {lowResStructure.Id} now");
            ContourLowResStructure(structurePoints, lowResStructure, startSlice, stopSlice);///, ProvideUIUpdate);
            return false;
        }

        /// <summary>
        /// Similar to the contourlowresstructure method in generatetsbase, except instead of supplying the high res structure as an
        /// input argument, the contour points for the high res structure are directly supplied
        /// </summary>
        /// <param name="structurePoints"></param>
        /// <param name="lowResStructure"></param>
        /// <param name="startSlice"></param>
        /// <param name="stopSlice"></param>
        /// <returns></returns>
        private static bool ContourLowResStructure(VVector[][][] structurePoints, Structure lowResStructure, int startSlice, int stopSlice)//, ProvideUIUpdateDelegate ProvideUIUpdate)
        {
            //ProvideUIUpdate(0, $"Contouring {lowResStructure.Id}:");
            //Write the high res contour points on the newly added low res structure
            int percentComplete = 0;
            int calcItems = stopSlice - startSlice + 1;
            for (int slice = startSlice; slice <= stopSlice; slice++)
            {
                VVector[][] points = structurePoints[percentComplete];
                for (int i = 0; i < points.GetLength(0); i++)
                {
                    if (lowResStructure.IsPointInsideSegment(points[i][0]) ||
                        lowResStructure.IsPointInsideSegment(points[i][points[i].GetLength(0) - 1]) ||
                        lowResStructure.IsPointInsideSegment(points[i][(int)(points[i].GetLength(0) / 2)]))
                    {
                        lowResStructure.SubtractContourOnImagePlane(points[i], slice);
                    }
                    else lowResStructure.AddContourOnImagePlane(points[i], slice);
                }
                //ProvideUIUpdate(100 * ++percentComplete / calcItems);
            }
            return false;
        }

        /// <summary>
        /// Helper method to retrive the contour points for the supplied structure on all contoured CT slices
        /// </summary>
        /// <param name="theStructure"></param>
        /// <param name="startSlice"></param>
        /// <param name="stopSlice"></param>
        /// <returns></returns>
        public static VVector[][][] GetAllContourPoints(Structure theStructure, int startSlice, int stopSlice)//, ProvideUIUpdateDelegate ProvideUIUpdate)
        {
            int percentComplete = 0;
            int calcItems = stopSlice - startSlice + 1;
            VVector[][][] structurePoints = new VVector[stopSlice - startSlice + 1][][];
            for (int slice = startSlice; slice <= stopSlice; slice++)
            {
                structurePoints[percentComplete++] = theStructure.GetContoursOnImagePlane(slice);
                //ProvideUIUpdate(100 * percentComplete / calcItems);
            }
            return structurePoints;
        }

        /// <summary>
        /// Helper method to remove the supplied high resolution structure, then add a new structure with the same id as the high resolution 
        /// structure (automatically defaults to default resolution)
        /// </summary>
        /// <param name="theStructure"></param>
        /// <returns></returns>
        private static (bool, Structure) RemoveAndReAddStructure(Structure theStructure, StructureSet selectedSS)//, ProvideUIUpdateDelegate ProvideUIUpdate)
        {
            //ProvideUIUpdate(0, "Removing and re-adding structure:");
            Structure newStructure = null;
            string id = string.Format("{0}_lowRes",theStructure.Id);
            string dicomType = theStructure.DicomType;
            System.Windows.Media.Color color = theStructure.Color;
            ///if (selectedSS.CanRemoveStructure(theStructure))
            ///{
            //selectedSS.RemoveStructure(theStructure);
            if (selectedSS.CanAddStructure(dicomType, id))
            {
                newStructure = selectedSS.AddStructure(dicomType, id);
                newStructure.Color = color;
                //ProvideUIUpdate(0, $"{newStructure.Id} has been added to the structure set");
            }
            else
            {
                //ProvideUIUpdate(0, $"Could not re-add structure: {id}. Exiting", true);
                return (true, newStructure);
            }
            //}
            //else
            //{
            //    ProvideUIUpdate(0,$"Could not remove structure: {id}. Exiting", true);
            //    return (true, newStructure);
            //}
            return (false, newStructure);
        }

        public static class CalculationHelper
        {
            /// <summary>
            /// Determine if x and y lengths are equivalent within tolerance (default value of 1 um)
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="tolerance"></param>
            /// <returns></returns>
            public static bool AreEqual(double x, double y, double tolerance = 0.001)
            {
                bool equal = false;
                double squareDiff = Math.Pow(x - y, 2);
                if (Math.Sqrt(squareDiff) <= tolerance) equal = true;
                return equal;
            }

            /// <summary>
            /// Compute mean of x and y (not included in Math library)
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static double ComputeAverage(double x, double y)
            {
                return (x + y) / 2;
            }

            /// <summary>
            /// Helper method to compute which CT slice a given z position is located
            /// </summary>
            /// <param name="z"></param>
            /// <param name="ss"></param>
            /// <returns></returns>
            public static int ComputeSlice(double z, StructureSet ss)
            {
                return (int)Math.Round((z - ss.Image.Origin.z) / ss.Image.ZRes);
            }
        }
    }
}


