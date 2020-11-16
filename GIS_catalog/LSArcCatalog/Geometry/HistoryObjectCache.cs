using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace LSArcCatalog
{
    /// <summary>
    /// HistoryObjectCache 是一个缓存列表。
    /// 在这个列表中记录了对象的变化状态。
    /// </summary>
    public class HistoryObjectCache
    {
        public HistoryObjectCache()
        {
            this.m_historyObjectList = new ArrayList();
        }
        private ArrayList m_historyObjectList;
        public void ClearCache()
        {
            this.m_historyObjectList.Clear();
        }
        #region 获取对象
        public int ObjectCount
        {
            get
            {
                return this.m_historyObjectList.Count;
            }
        }
        public int IndexOf(object pObject)
        {
            int oCount = this.ObjectCount;
            for (int oi = 0; oi < oCount; oi++)
            {
                HistoryObject hisObj = this.GetObjectAt(oi);
                if (hisObj.ContentObject.Equals(pObject))
                {
                    return oi;
                }
            }
            return -1;
        }
        public HistoryObject GetObjectAt(int pIndex)
        {
            return this.m_historyObjectList[pIndex] as HistoryObject;
        }
        #endregion
        public void PutOrigin(object pObject)
        {
            if (pObject != null)
            {
                HistoryObject hisObj = new HistoryObject(HistoryState.Origin, pObject);
                this.m_historyObjectList.Add(hisObj);
            }
        }
        public void AddNew(object pNewObject)
        {
            if (pNewObject != null)
            {
                HistoryObject hisObj = new HistoryObject(HistoryState.New, pNewObject);
                //这里有问题.
                if (!this.m_historyObjectList.Contains(hisObj))
                {
                    this.m_historyObjectList.Add(hisObj);
                }
            }
        }
        public void Replace(object pOldObject, object pNewObject)
        {
            int index = this.IndexOf(pOldObject);
            if (index >= 0)
            {
                HistoryObject hisObj = this.GetObjectAt(index);
                if (hisObj.HistoryState != HistoryState.New)
                {//如果是新的那么就没有update。                
                    hisObj.HistoryState = HistoryState.Updated;
                }
                hisObj.ContentObject = pNewObject;
            }
        }
        public void Delete(object pObject)
        {
            int index = this.IndexOf(pObject);
            if (index >= 0)
            {
                HistoryObject hisObj = this.GetObjectAt(index);
                if (hisObj.HistoryState == HistoryState.New)
                {//新建的被删除了。没有什么可以需要记录的了。
                    this.m_historyObjectList.RemoveAt(index);
                }
                else
                {
                    hisObj.HistoryState = HistoryState.Deleted;
                }

            }
        }
        public bool Changed
        {
            get
            {
                foreach (HistoryObject hisObj in this.m_historyObjectList)
                {
                    if (hisObj != null)
                    {
                        if (HistoryState.New == hisObj.HistoryState
                        || HistoryState.Updated == hisObj.HistoryState
                        || HistoryState.Deleted == hisObj.HistoryState)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public List<object> ResultObjectList
        {
            get
            {
                List<object> rList = new List<object>();
                foreach (HistoryObject hisObj in this.m_historyObjectList)
                {
                    if (hisObj != null)
                    {
                        if (hisObj.HistoryState != HistoryState.Deleted)
                        {
                            rList.Add(hisObj.ContentObject);
                        }
                    }
                }
                return rList;
            }
        }
    }
    public class HistoryObject
    {
        public HistoryState HistoryState;
        public object ContentObject;
        public HistoryObject(HistoryState pState, object pObject)
        {
            this.HistoryState = pState;
            this.ContentObject = pObject;
        }
        public override bool Equals(object obj)
        {
            if (obj is HistoryObject)
            {
                HistoryObject hisObj = obj as HistoryObject;
                return this.ContentObject.Equals(hisObj.ContentObject);
            }
            return false;
        }

    }
    public enum HistoryState
    {
        Origin = 0,//原来就有，一直没有变化
        New = 1,//新添加的
        Updated = 2,//修改了的
        Deleted = 3//删除了的
    }
}
