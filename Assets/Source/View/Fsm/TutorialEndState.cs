using Model;
using UnityEngine.SceneManagement;

namespace View
{
    public class TutorialEndState : FsmState
    {
        public TutorialEndState(Fsm fsm) : base(fsm) { }

        public override void Enter()
        {
            DataSave dataSave = new DataSave();
            Data data = dataSave.Load();
            data = data == null ? new Data() : data;

            data.isStart = true;
            dataSave.Save(data);

            SceneManager.LoadScene(Constant.GamplayScenes);
        }
    }
}
