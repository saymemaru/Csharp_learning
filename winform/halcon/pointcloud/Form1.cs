using HalconDotNet;
using PointCloud;

namespace pointcloud
{
    public partial class Form1 : Form
    {
        private HDevelopExport export = new();

        private Task visualizationTask;

        private HWindowControl hwControl;
        private HWindow hWindow;
        private HTuple hv_ObjectModel3D = new HTuple(), hv_Status = new HTuple();
        private HTuple hv_RawSence = new HTuple(), hv_ParamValue = new HTuple();
        private HTuple hv_WindowHandle = new HTuple(), hv_PoseOut = new HTuple();
        private HTuple hv_TriangulatedRawSence = new HTuple(), hv_Information = new HTuple();
        private HTuple hv_SampledModel = new HTuple(), hv_ObjectModel3DConnected = new HTuple();
        private HTuple hv_ObjectModel3DSelected = new HTuple(), hv_CorrectionPose = new HTuple();
        private HTuple hv_Moments = new HTuple(), hv_PoseInvert = new HTuple();
        private HTuple hv_ObjectModel3DRigidTrans = new HTuple(), hv_TriangulatedTemplateModel = new HTuple();
        private HTuple hv_SFM = new HTuple(), hv_Pose = new HTuple(), hv_Score = new HTuple();
        private HTuple hv_SurfaceMatchingResultID = new HTuple(), hv_ObjectModel3DResult = new HTuple();
        private HTuple hv_Index = new HTuple(), hv_CurrentPose = new HTuple();
        private HTuple hv_MatchedInstance = new HTuple();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化halcon控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //创建halcon控件
            hwControl = new HWindowControl();

            //获取halcon控件上的窗口对象
            hWindow = hwControl.HalconWindow;
            //窗口对象入栈
            HDevWindowStack.Push(hWindow);

            //halcon荣建填充于容器中
            hwControl.Dock = DockStyle.Fill;
            //添加控件到容器中
            panel1.Controls.Add(hwControl);
        }
        /// <summary>
        /// 加载模板点云文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"E:/work/FutureRay/3dcamera/pic/test2";
            dialog.Filter = "打开点云文件|*.ply";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //获取选择的文件路径
                var filePath = dialog.FileName;
                //读取模板点云文件
                hv_ObjectModel3D.Dispose(); hv_Status.Dispose();
                HOperatorSet.ReadObjectModel3d(
                    filePath, "m",
                    new HTuple(), new HTuple(),
                    out hv_ObjectModel3D, out hv_Status);
            }
        }

        /// <summary>
        /// 分割点云模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            HTuple ExpTmpOutVar_0;
            //按坐标分割点云（目前为预设值）
            HOperatorSet.SelectPointsObjectModel3d(
                hv_ObjectModel3D,
                ((new HTuple("point_coord_z")).TupleConcat("point_coord_y")).TupleConcat("point_coord_x"),
                ((new HTuple(300)).TupleConcat(-30)).TupleConcat(-140),
                ((new HTuple(467)).TupleConcat(10)).TupleConcat(-90),
                out ExpTmpOutVar_0);

            hv_ObjectModel3D.Dispose();
            hv_ObjectModel3D = ExpTmpOutVar_0;

        }

        /// <summary>
        /// 显示当前模板点云
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //下采样简化模型
            hv_SampledModel.Dispose();
            HOperatorSet.SampleObjectModel3d(hv_ObjectModel3D, "fast", 1, new HTuple(),
                new HTuple(), out hv_SampledModel);

            //显示模型(多线程)
            visualizationTask = Task.Run(() =>
            {
                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hWindow, hv_SampledModel, new HTuple(), new HTuple(),
                ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat("disp_pose"),
                ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat("true"), new HTuple(),
                new HTuple(), new HTuple(), out hv_PoseOut);

                hWindow.ClearWindow();
            });

        }

        /// <summary>
        /// 去噪,对齐到主轴坐标系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void button4_Click(object sender, EventArgs e)
        {
            //基于距离去噪,断开孤立点
            hv_ObjectModel3DConnected.Dispose();
            HOperatorSet.ConnectionObjectModel3d(hv_ObjectModel3D, "distance_3d", 1, out hv_ObjectModel3DConnected);
            hv_ObjectModel3DSelected.Dispose();
            HOperatorSet.SelectObjectModel3d(hv_ObjectModel3DConnected, "num_points", "and",
                4000, 2e7, out hv_ObjectModel3DSelected);

            //创建坐标系修正位姿
            hv_CorrectionPose.Dispose();
            HOperatorSet.CreatePose(0, -18, 0, 0, 0, -9, "Rp+T", "gba", "point", out hv_CorrectionPose);
            //对齐到主轴坐标系
            hv_Moments.Dispose();
            HOperatorSet.MomentsObjectModel3d(hv_ObjectModel3DSelected, "principal_axes",
                out hv_Moments);
            hv_PoseInvert.Dispose();
            HOperatorSet.PoseInvert(hv_Moments, out hv_PoseInvert);
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.PoseCompose(hv_PoseInvert, hv_CorrectionPose, out ExpTmpOutVar_0);
                hv_PoseInvert.Dispose();
                hv_PoseInvert = ExpTmpOutVar_0;
            }
            hv_ObjectModel3DRigidTrans.Dispose();
            HOperatorSet.RigidTransObjectModel3d(hv_ObjectModel3DSelected, hv_PoseInvert,
                out hv_ObjectModel3DRigidTrans);

            //显示
            visualizationTask = Task.Run(() =>
            {
                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hWindow, hv_ObjectModel3DRigidTrans, new HTuple(),
                    new HTuple(), ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat(
                    "disp_pose"), ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat(
                    "true"), new HTuple(), new HTuple(), new HTuple(), out hv_PoseOut);

                hWindow.ClearWindow();
            });

        }

        /// <summary>
        /// 三角化并创建模板并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //三角化模板
            hv_TriangulatedTemplateModel.Dispose(); hv_Information.Dispose();
            HOperatorSet.TriangulateObjectModel3d(hv_ObjectModel3DSelected, "greedy", new HTuple(),
                new HTuple(), out hv_TriangulatedTemplateModel, out hv_Information);

            //创建模板
            hv_SFM.Dispose();
            HOperatorSet.CreateSurfaceModel(hv_TriangulatedTemplateModel, 0.03, new HTuple(),
                new HTuple(), out hv_SFM);

            //显示模板
           /* hv_PoseOut.Dispose();
            visualizationTask = Task.Run(() =>
            {
                export.visualize_object_model_3d(hWindow, hv_SFM, new HTuple(),
                new HTuple(), ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat(
                "disp_pose"), ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat(
                "true"), new HTuple(), new HTuple(), new HTuple(), out hv_PoseOut);
            });*/
        }

        /// <summary>
        /// 选择背景模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            //选择文件
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"E:/work/FutureRay/3dcamera/pic/test2";
            dialog.Filter = "打开点云文件|*.ply";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //获取选择的文件路径
                var filePath = dialog.FileName;
                //读取背景点云文件
                hv_ObjectModel3D.Dispose(); hv_Status.Dispose();
                HOperatorSet.ReadObjectModel3d(
                    filePath, "m",
                    new HTuple(), new HTuple(),
                    out hv_RawSence, out hv_Status);
            }

            HOperatorSet.SelectPointsObjectModel3d(hv_RawSence, ((new HTuple("point_coord_z")).TupleConcat(
                "point_coord_y")).TupleConcat("point_coord_x"), ((new HTuple(300)).TupleConcat(
                -100)).TupleConcat(-300), ((new HTuple(500)).TupleConcat(100)).TupleConcat(
                -50), out hv_RawSence);

            hv_TriangulatedRawSence.Dispose(); hv_Information.Dispose();
            HOperatorSet.TriangulateObjectModel3d(hv_RawSence, "greedy", new HTuple(),
                new HTuple(), out hv_TriangulatedRawSence, out hv_Information);
        }

        /// <summary>
        /// 匹配并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            //4. 执行表面匹配
            //参数：
            //  - 0.2: 采样步长（与create时一致）
            //  - 0.5: 关键点比率
            //  - 0.8: 最小匹配分数阈值（可降低以提高召回率）
            hv_Pose.Dispose(); hv_Score.Dispose(); hv_SurfaceMatchingResultID.Dispose();
            HOperatorSet.FindSurfaceModel(hv_SFM, hv_TriangulatedRawSence, 0.03, 0.1, 0.3,
                "true", "num_matches", 2, out hv_Pose, out hv_Score, out hv_SurfaceMatchingResultID);

            //5. 结果处理与可视化
            //提取高质量匹配结果（分数>0.15）
            hv_ObjectModel3DResult.Dispose();
            hv_ObjectModel3DResult = new HTuple();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Score.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                if ((int)(new HTuple(((hv_Score.TupleSelect(hv_Index))).TupleLess(0.15))) != 0)
                {
                    continue;
                }
                //获取当前匹配位姿（X,Y,Z + 四元数旋转）
                hv_CurrentPose.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_CurrentPose = hv_Pose.TupleSelectRange(
                        hv_Index * 7, (hv_Index * 7) + 6);
                }

                //将模板变换到匹配位姿，并保存
                hv_MatchedInstance.Dispose();
                HOperatorSet.RigidTransObjectModel3d(hv_TriangulatedTemplateModel, hv_CurrentPose,
                    out hv_MatchedInstance);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ObjectModel3DResult = hv_ObjectModel3DResult.TupleConcat(
                            hv_MatchedInstance);
                        hv_ObjectModel3DResult.Dispose();
                        hv_ObjectModel3DResult = ExpTmpLocalVar_ObjectModel3DResult;
                    }
                }
            }


            //可视化显示（分层渲染）
            //图层0：原始场景（半透明蓝色）
            //图层1：匹配结果（红色实体） 
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hWindow, hv_TriangulatedRawSence.TupleConcat(
                    hv_ObjectModel3DResult), new HTuple(), new HTuple(), ((new HTuple("color_0")).TupleConcat(
                    "color_1")).TupleConcat("alpha_0"), ((new HTuple("blue")).TupleConcat("red")).TupleConcat(
                    0.6), new HTuple(), new HTuple(), new HTuple(), out hv_PoseOut);
            }

        }
        private void button10_Click(object sender, EventArgs e)
        {

            HTuple hv_ObjectModel3D = new HTuple(), hv_Status = new HTuple();
            HTuple hv_RawSence = new HTuple(), hv_ParamValue = new HTuple();
            HTuple hv_WindowHandle = new HTuple(), hv_PoseOut = new HTuple();
            HTuple hv_TriangulatedRawSence = new HTuple(), hv_Information = new HTuple();
            HTuple hv_SampledModel = new HTuple(), hv_ObjectModel3DConnected = new HTuple();
            HTuple hv_ObjectModel3DSelected = new HTuple(), hv_CorrectionPose = new HTuple();
            HTuple hv_Moments = new HTuple(), hv_PoseInvert = new HTuple();
            HTuple hv_ObjectModel3DRigidTrans = new HTuple(), hv_TriangulatedTemplateModel = new HTuple();
            HTuple hv_SFM = new HTuple(), hv_Pose = new HTuple(), hv_Score = new HTuple();
            HTuple hv_SurfaceMatchingResultID = new HTuple(), hv_ObjectModel3DResult = new HTuple();
            HTuple hv_Index = new HTuple(), hv_CurrentPose = new HTuple();
            HTuple hv_MatchedInstance = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                //1. ==== 预处理 ====
                //1. 读取内置模型
                hv_ObjectModel3D.Dispose(); hv_Status.Dispose();
                HOperatorSet.ReadObjectModel3d("E:/work/FutureRay/3dcamera/pic/test2/point.ply",
                    "m", new HTuple(), new HTuple(), out hv_ObjectModel3D, out hv_Status);
                hv_RawSence.Dispose(); hv_Status.Dispose();
                HOperatorSet.ReadObjectModel3d("E:/work/FutureRay/3dcamera/pic/test2/point2.ply",
                    "m", new HTuple(), new HTuple(), out hv_RawSence, out hv_Status);
                hv_ParamValue.Dispose();
                HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3D, "point_coord_z", out hv_ParamValue);
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(0, 0, 512, 512, 0, "visible", "", out hv_WindowHandle);
                HDevWindowStack.Push(hv_WindowHandle);


                //3. 模拟场景数据生成测试场景
                //1未分割桌面 2分割桌面 3小面积桌面
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.SelectPointsObjectModel3d(hv_RawSence, ((new HTuple("point_coord_z")).TupleConcat(
                        "point_coord_y")).TupleConcat("point_coord_x"), ((new HTuple(300)).TupleConcat(
                        -100)).TupleConcat(-300), ((new HTuple(500)).TupleConcat(100)).TupleConcat(
                        -50), out ExpTmpOutVar_0);
                    hv_RawSence.Dispose();
                    hv_RawSence = ExpTmpOutVar_0;
                }
                //select_points_object_model_3d (ObjectModel3D, ['point_coord_z','point_coord_y','point_coord_x'], [300, -100, -300], [467, 100, -50], RawSence)

                //select_points_object_model_3d (ObjectModel3D, ['point_coord_z','point_coord_y','point_coord_x'], [400,-30,-140], [500,10,-90], RawSence)

                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hv_WindowHandle, hv_RawSence, new HTuple(), new HTuple(),
                    ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat("disp_pose"),
                    ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat("true"), new HTuple(),
                    new HTuple(), new HTuple(), out hv_PoseOut);
                hv_TriangulatedRawSence.Dispose(); hv_Information.Dispose();
                HOperatorSet.TriangulateObjectModel3d(hv_RawSence, "greedy", new HTuple(),
                    new HTuple(), out hv_TriangulatedRawSence, out hv_Information);

                //选择各轴某段距离上的所有点（可选xyz轴，去除工件以外区域的点）


                //select_points_object_model_3d (ObjectModel3D, ['point_coord_z','point_coord_y','point_coord_x'], [300, -100, -300], [467, 100, -50], ObjectModel3D)
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.SelectPointsObjectModel3d(hv_ObjectModel3D, ((new HTuple("point_coord_z")).TupleConcat(
                        "point_coord_y")).TupleConcat("point_coord_x"), ((new HTuple(300)).TupleConcat(
                        -30)).TupleConcat(-140), ((new HTuple(467)).TupleConcat(10)).TupleConcat(
                        -90), out ExpTmpOutVar_0);
                    hv_ObjectModel3D.Dispose();
                    hv_ObjectModel3D = ExpTmpOutVar_0;
                }

                //2. 下采样
                hv_SampledModel.Dispose();
                HOperatorSet.SampleObjectModel3d(hv_ObjectModel3D, "fast", 1, new HTuple(),
                    new HTuple(), out hv_SampledModel);

                //显示点云数据
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(0, 0, 512, 512, 0, "visible", "", out hv_WindowHandle);
                HDevWindowStack.Push(hv_WindowHandle);
                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hv_WindowHandle, hv_SampledModel, new HTuple(), new HTuple(),
                    ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat("disp_pose"),
                    ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat("true"), new HTuple(),
                    new HTuple(), new HTuple(), out hv_PoseOut);

                //基于距离去噪（断开孤立点）(最小/最大数量)
                hv_ObjectModel3DConnected.Dispose();
                HOperatorSet.ConnectionObjectModel3d(hv_ObjectModel3D, "distance_3d", 1, out hv_ObjectModel3DConnected);
                hv_ObjectModel3DSelected.Dispose();
                HOperatorSet.SelectObjectModel3d(hv_ObjectModel3DConnected, "num_points", "and",
                    4000, 2e7, out hv_ObjectModel3DSelected);
                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hv_WindowHandle, hv_ObjectModel3DSelected, new HTuple(),
                    new HTuple(), ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat(
                    "disp_pose"), ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat(
                    "true"), new HTuple(), new HTuple(), new HTuple(), out hv_PoseOut);


                //创建坐标系修正位姿
                hv_CorrectionPose.Dispose();
                HOperatorSet.CreatePose(0, -18, 0, 0, 0, -9, "Rp+T", "gba", "point", out hv_CorrectionPose);
                //对齐到主轴坐标系
                hv_Moments.Dispose();
                HOperatorSet.MomentsObjectModel3d(hv_ObjectModel3DSelected, "principal_axes",
                    out hv_Moments);
                hv_PoseInvert.Dispose();
                HOperatorSet.PoseInvert(hv_Moments, out hv_PoseInvert);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.PoseCompose(hv_PoseInvert, hv_CorrectionPose, out ExpTmpOutVar_0);
                    hv_PoseInvert.Dispose();
                    hv_PoseInvert = ExpTmpOutVar_0;
                }
                hv_ObjectModel3DRigidTrans.Dispose();
                HOperatorSet.RigidTransObjectModel3d(hv_ObjectModel3DSelected, hv_PoseInvert,
                    out hv_ObjectModel3DRigidTrans);
                hv_PoseOut.Dispose();
                export.visualize_object_model_3d(hv_WindowHandle, hv_ObjectModel3DRigidTrans, new HTuple(),
                    new HTuple(), ((new HTuple("lut")).TupleConcat("intensity")).TupleConcat(
                    "disp_pose"), ((new HTuple("color1")).TupleConcat("coord_z")).TupleConcat(
                    "true"), new HTuple(), new HTuple(), new HTuple(), out hv_PoseOut);

                //三角化重建表面
                hv_TriangulatedTemplateModel.Dispose(); hv_Information.Dispose();
                HOperatorSet.TriangulateObjectModel3d(hv_ObjectModel3DSelected, "greedy", new HTuple(),
                    new HTuple(), out hv_TriangulatedTemplateModel, out hv_Information);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.SelectObjectModel3d(hv_TriangulatedTemplateModel, "num_triangles",
                        "and", 5000, 10000, out ExpTmpOutVar_0);
                    hv_TriangulatedTemplateModel.Dispose();
                    hv_TriangulatedTemplateModel = ExpTmpOutVar_0;
                }
                //==== 2. 创建表面匹配模型 ====
                //参数：0.2=采样步长（需与后续匹配一致）
                hv_SFM.Dispose();
                HOperatorSet.CreateSurfaceModel(hv_TriangulatedTemplateModel, 0.03, new HTuple(),
                    new HTuple(), out hv_SFM);


                //4. 执行表面匹配
                //参数：
                //  - 0.2: 采样步长（与create时一致）
                //  - 0.5: 关键点比率
                //  - 0.8: 最小匹配分数阈值（可降低以提高召回率）
                hv_Pose.Dispose(); hv_Score.Dispose(); hv_SurfaceMatchingResultID.Dispose();
                HOperatorSet.FindSurfaceModel(hv_SFM, hv_TriangulatedRawSence, 0.03, 0.1, 0.3,
                    "true", "num_matches", 2, out hv_Pose, out hv_Score, out hv_SurfaceMatchingResultID);

                //5. 结果处理与可视化
                //提取高质量匹配结果（分数>0.15）
                hv_ObjectModel3DResult.Dispose();
                hv_ObjectModel3DResult = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Score.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((hv_Score.TupleSelect(hv_Index))).TupleLess(0.15))) != 0)
                    {
                        continue;
                    }
                    //获取当前匹配位姿（X,Y,Z + 四元数旋转）
                    hv_CurrentPose.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentPose = hv_Pose.TupleSelectRange(
                            hv_Index * 7, (hv_Index * 7) + 6);
                    }

                    //将模板变换到匹配位姿，并保存
                    hv_MatchedInstance.Dispose();
                    HOperatorSet.RigidTransObjectModel3d(hv_TriangulatedTemplateModel, hv_CurrentPose,
                        out hv_MatchedInstance);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ObjectModel3DResult = hv_ObjectModel3DResult.TupleConcat(
                                hv_MatchedInstance);
                            hv_ObjectModel3DResult.Dispose();
                            hv_ObjectModel3DResult = ExpTmpLocalVar_ObjectModel3DResult;
                        }
                    }
                }


                //可视化显示（分层渲染）
                //图层0：原始场景（半透明蓝色）
                //图层1：匹配结果（红色实体）
                HDevWindowStack.SetActive(hv_WindowHandle);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PoseOut.Dispose();
                    export.visualize_object_model_3d(hv_WindowHandle, hv_TriangulatedRawSence.TupleConcat(
                        hv_ObjectModel3DResult), new HTuple(), new HTuple(), ((new HTuple("color_0")).TupleConcat(
                        "color_1")).TupleConcat("alpha_0"), ((new HTuple("blue")).TupleConcat("red")).TupleConcat(
                        0.6), new HTuple(), new HTuple(), new HTuple(), out hv_PoseOut);
                }



            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ObjectModel3D.Dispose();
                hv_Status.Dispose();
                hv_RawSence.Dispose();
                hv_ParamValue.Dispose();
                hv_WindowHandle.Dispose();
                hv_PoseOut.Dispose();
                hv_TriangulatedRawSence.Dispose();
                hv_Information.Dispose();
                hv_SampledModel.Dispose();
                hv_ObjectModel3DConnected.Dispose();
                hv_ObjectModel3DSelected.Dispose();
                hv_CorrectionPose.Dispose();
                hv_Moments.Dispose();
                hv_PoseInvert.Dispose();
                hv_ObjectModel3DRigidTrans.Dispose();
                hv_TriangulatedTemplateModel.Dispose();
                hv_SFM.Dispose();
                hv_Pose.Dispose();
                hv_Score.Dispose();
                hv_SurfaceMatchingResultID.Dispose();
                hv_ObjectModel3DResult.Dispose();
                hv_Index.Dispose();
                hv_CurrentPose.Dispose();
                hv_MatchedInstance.Dispose();

                throw HDevExpDefaultException;
            }

            hv_ObjectModel3D.Dispose();
            hv_Status.Dispose();
            hv_RawSence.Dispose();
            hv_ParamValue.Dispose();
            hv_WindowHandle.Dispose();
            hv_PoseOut.Dispose();
            hv_TriangulatedRawSence.Dispose();
            hv_Information.Dispose();
            hv_SampledModel.Dispose();
            hv_ObjectModel3DConnected.Dispose();
            hv_ObjectModel3DSelected.Dispose();
            hv_CorrectionPose.Dispose();
            hv_Moments.Dispose();
            hv_PoseInvert.Dispose();
            hv_ObjectModel3DRigidTrans.Dispose();
            hv_TriangulatedTemplateModel.Dispose();
            hv_SFM.Dispose();
            hv_Pose.Dispose();
            hv_Score.Dispose();
            hv_SurfaceMatchingResultID.Dispose();
            hv_ObjectModel3DResult.Dispose();
            hv_Index.Dispose();
            hv_CurrentPose.Dispose();
            hv_MatchedInstance.Dispose();

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }

 }
