using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UnityAircraft.Game.Attribute;
using UnityAircraft.Game.Extensions;
using UnityAircraft.Test.Utility;
using UnityEngine;

namespace UnityAircraft.Test.InputTest
{
    public class InputTestBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private TextMeshProUGUI _gunText;
        [SerializeField] private TextMeshProUGUI _launchText;
        [SerializeField] [Button] private bool _reset;

        private float _pitch;
        private float _roll;
        private float _yaw;
        private CancellationDisposable _launchCancellationDisposable;

        private void Start()
        {
            _launchText.enabled = false;
        }

        private void Update()
        {
            TestUtility.DoOnce(ref _reset, () => _model.rotation = Quaternion.identity);

            _model.Rotate(Vector3.right, _pitch);
            _model.Rotate(Vector3.forward, _roll);
            _model.Rotate(Vector3.up, _yaw);
        }

        public void SetPitch(float pitch)
        {
            _pitch = pitch;
        }

        public void SetRoll(float roll)
        {
            _roll = roll;
        }

        public void SetYaw(float yaw)
        {
            _yaw = yaw;
        }

        public void SetGun(bool gun)
        {
            _gunText.enabled = gun;
        }

        public void ShowLaunch(float duration)
        {
            _launchText.enabled = true;

            _launchCancellationDisposable?.Dispose();
            _launchCancellationDisposable = new CancellationDisposable();
            HideLaunchAsync(TimeSpan.FromSeconds(duration), _launchCancellationDisposable.Token).Forget();
        }

        private async UniTask HideLaunchAsync(TimeSpan delay, CancellationToken cancellation)
        {
            var linkedToken = destroyCancellationToken.LinkWith(cancellation);
            await UniTask.Delay(delay, cancellationToken: linkedToken);
            _launchText.enabled = false;
        }
    }
}
