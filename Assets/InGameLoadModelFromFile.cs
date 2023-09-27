using TriLibCore.General;
using UnityEngine;
using TMPro;
using TriLibCore.Extensions;
using TriLibCore.Mappers;

namespace TriLibCore.Samples
{
    /// <summary>
    /// Represents a sample that loads the "TriLibSample.obj" Model from the "Models" folder.
    /// </summary>
    public class InGameLoadModelFromFile : AssetViewerBase
    {
        [SerializeField] private string modelPath;
        [SerializeField] private TextMeshProUGUI txtLoadMessage;
        //[SerializeField] private HumanoidAvatarMapper _humanoidAvatarMapper;

        /// <remarks>
        /// You can create the AssetLoaderOptions by right clicking on the Assets Explorer and selecting "TriLib->Create->AssetLoaderOptions->Pre-Built AssetLoaderOptions".
        /// </remarks>
        protected override void Start()
        {
            base.Start();
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            AssetLoader.LoadModelFromFile(modelPath, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions);
            _loadingWrapper.SetActive(true);
            //assetLoaderOptions.AnimationType = AnimationType.Humanoid;
            //assetLoaderOptions.HumanoidAvatarMapper = _humanoidAvatarMapper;
            //var bounds = AvatarController.Instance.InnerAvatar.CalculateBounds();
            //var factor = AvatarController.Instance.CharacterController.height / bounds.size.y;
            //AvatarController.Instance.InnerAvatar.transform.localScale = factor * Vector3.one;
        }

        /// <summary>
        /// Called when any error occurs.
        /// </summary>
        /// <param name="obj">The contextualized error, containing the original exception and the context passed to the method where the error was thrown.</param>
        protected override void OnError(IContextualizedError obj)
        {
            base.OnError(obj);
            Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
        }

        /// <summary>
        /// Called when the Model loading progress changes.
        /// </summary>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        /// <param name="progress">The loading progress.</param>
        protected override void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            base.OnProgress(assetLoaderContext, progress);
            txtLoadMessage.text = $"Loading Model... {progress:P}";
            //Debug.Log($"Loading Model. Progress: {progress:P}");
        }

        /// <summary>
        /// Called when the Model (including Textures and Materials) has been fully loaded.
        /// </summary>
        /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        protected override void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            base.OnMaterialsLoad(assetLoaderContext);
            txtLoadMessage.text = "Materials loaded. Model fully loaded.";
            _loadingWrapper.SetActive(false);

            //if (assetLoaderContext.RootGameObject != null)
            //{
            //    var existingInnerAvatar = AvatarController.Instance.InnerAvatar;
            //    if (existingInnerAvatar != null)
            //    {
            //        Destroy(existingInnerAvatar);
            //    }
            //    var controller = AvatarController.Instance.Animator.runtimeAnimatorController;
            //    var bounds = assetLoaderContext.RootGameObject.CalculateBounds();
            //    var factor = AvatarController.Instance.CharacterController.height / bounds.size.y;
            //    assetLoaderContext.RootGameObject.transform.localScale = factor * Vector3.one;
            //    AvatarController.Instance.InnerAvatar = assetLoaderContext.RootGameObject;
            //    assetLoaderContext.RootGameObject.transform.SetParent(AvatarController.Instance.transform, false);
            //    AvatarController.Instance.Animator = assetLoaderContext.RootGameObject.GetComponent<Animator>();
            //    AvatarController.Instance.Animator.runtimeAnimatorController = controller;
            //}
            //Debug.Log("Materials loaded. Model fully loaded.");
        }

        /// <summary>
        /// Called when the Model Meshes and hierarchy are loaded.
        /// </summary>
        /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        protected override void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            base.OnLoad(assetLoaderContext);
            txtLoadMessage.text = "Model loaded. Loading materials.";
            //Debug.Log("Model loaded. Loading materials.");
        }
    }
}
