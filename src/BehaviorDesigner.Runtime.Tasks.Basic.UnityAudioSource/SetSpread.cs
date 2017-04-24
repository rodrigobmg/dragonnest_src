using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	[TaskCategory("Basic/AudioSource"), TaskDescription("Sets the spread value of the AudioSource. Returns Success.")]
	public class SetSpread : BehaviorDesigner.Runtime.Tasks.Action
	{
		[BehaviorDesigner.Runtime.Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		[BehaviorDesigner.Runtime.Tasks.Tooltip("The spread value of the AudioSource")]
		public SharedFloat spread;

		private AudioSource audioSource;

		private GameObject prevGameObject;

		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return TaskStatus.Failure;
			}
			this.audioSource.spread = this.spread.Value;
			return TaskStatus.Success;
		}

		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spread = 1f;
		}
	}
}