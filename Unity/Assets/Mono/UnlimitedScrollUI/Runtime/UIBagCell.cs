using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnlimitedScrollUI;

namespace UnlimitedScrollUI
{
    public class UIBagCell : MonoBehaviour, ICell
    {

        public event Action<ScrollerPanelSide> on_BecomeInvisible;
        public event Action<ScrollerPanelSide> on_BecomeVisible;

        void ICell.OnBecomeInvisible(ScrollerPanelSide side)
        {
            on_BecomeInvisible?.Invoke(side);
        }

        void ICell.OnBecomeVisible(ScrollerPanelSide side)
        {
            on_BecomeVisible?.Invoke(side);
        }
    }
}
