using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loaderPanel;
    [SerializeField] private Image progressBar;
    [SerializeField] private ParticleSystem particle;
    //private float target;
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(ShowProgressBarAndLoadScene(sceneName));
    }
    private IEnumerator ShowProgressBarAndLoadScene(string sceneName)
    {
        // Hiển thị thanh progress bar
        loaderPanel.SetActive(true);
        particle?.Stop();
        particle?.Play();
        //Debug.Log("Progress bar container activated");

        // Buộc UI cập nhật ngay lập tức
        Canvas.ForceUpdateCanvases();

        // Đợi nhiều khung hình để đảm bảo UI được cập nhật
        yield return new WaitForSeconds(1f);

        // Bắt đầu tải scene bất đồng bộ
        yield return StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        //Debug.Log("here");
        //loaderPanel.SetActive(true);
        //particle?.Stop();
        //particle?.Play();
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        //while (!operation.isDone)
        //{
        //    float progressValue = Mathf.Clamp01(operation.progress / .9f);
        //    progressBar.fillAmount = progressValue;
        //    yield return null;
        //}
        //-------------------------------------------------------------------------
        // Hiển thị thanh progress bar
        //Debug.Log("now");
        //loaderPanel.SetActive(true);
        //particle?.Stop();
        //particle?.Play();
        //Canvas.ForceUpdateCanvases();

        //yield return null; // Đợi đến khung hình tiếp theo để đảm bảo UI được cập nhật

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Cập nhật progress bar trong khi tải
        while (!asyncLoad.isDone)
        {
            // Tiến trình tải từ 0 đến 0.9
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.fillAmount = progress;

            // Thêm trễ nhân tạo để thấy tiến trình
            yield return new WaitForSeconds(2f); // Thêm trễ 0.1 giây mỗi khung hình

            // Khi tiến trình tải gần hoàn thành (từ 0.9 trở đi)
            if (asyncLoad.progress >= 0.9f)
            {
                // Cập nhật thanh progress bar đến 1
                progressBar.fillAmount = 1f;

                // Đợi một thời gian ngắn trước khi chuyển cảnh
                yield return new WaitForSeconds(0.5f); // Tùy chọn: Thời gian chờ

                // Cho phép chuyển cảnh
                asyncLoad.allowSceneActivation = true;
            }
        
        }
        //-------------------------------------------------------------------------
        //target = 0;
        //progressBar.fillAmount = 0;
        //loaderPanel.SetActive(true);
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        //asyncLoad.allowSceneActivation = false;

        //// Tùy chọn: Hiển thị màn hình chờ hoặc thanh tiến trình tại đây
        //while (!asyncLoad.isDone)
        //{
        //    target = asyncLoad.progress;
        //    // Kiểm tra tiến trình tải
        //    if (asyncLoad.progress >= 0.9f)
        //    {
        //        // Tùy chọn: Cho phép người dùng bấm nút để kích hoạt cảnh mới
        //        loaderPanel.SetActive(false);
        //        asyncLoad.allowSceneActivation = true;
        //    }
        //    yield return null;
        //}
    }
    //void Update()
    //{
    //    progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, Time.deltaTime * 3);
    //}
}
