﻿using System;
using Cysharp.Threading.Tasks;
using Game.Inventory.Models;
using PamisuKit.Common.Util;
using PamisuKit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Inventory
{
    public class ItemSlot : MonoEntity, IPointerDownHandler
    {
        
        [Space]
        [SerializeField]
        protected Image FrameImage;

        [SerializeField]
        protected Sprite FrameEmptySprite;

        [SerializeField]
        protected Sprite FrameNormalSprite;

        [SerializeField]
        protected Image IconImage;
        
        [SerializeField]
        protected RectTransform AmountBadge;

        [SerializeField]
        protected TMP_Text AmountLabel;

        [SerializeField]
        protected GameObject DragDummyPrefab;

        private RectTransform _rectTrans;

        public ItemContainer Container { get; internal set; }
        public int Index { get; internal set; }

        private Item _item;
        public Item Item
        {
            get => _item;
            set
            {
                var oldItem = _item;
                if (_item != null)
                {
                    _item.Changed -= OnItemChanged;
                    _item.Removing -= OnItemRemoving;
                }
                _item = value;
                if (_item != null)
                {
                    _item.Changed += OnItemChanged;
                    _item.Removing += OnItemRemoving;
                }
                Changed?.Invoke(this, oldItem, _item);
            }
        }

        public event Action<ItemSlot, Item, Item> Changed; 

        protected override void OnCreate()
        {
            base.OnCreate();
            _rectTrans = Trans as RectTransform;
        }

        private void OnItemChanged(Item item)
        {
            AmountLabel.text = item.Amount.ToString();
        }
        
        private void OnItemRemoving(Item item)
        {
            var oldItem = _item;
            _item = null;
            Changed?.Invoke(this, oldItem, null);
            Refresh();
        }

        public void Refresh()
        {
            if (_item != null)
            {
                FrameImage.sprite = FrameNormalSprite;
                IconImage.color = Color.white;
                IconImage.LoadSprite(_item.Config.IconRef).Forget();
                AmountBadge.gameObject.SetActive(true);
                AmountLabel.text = _item.Amount.ToString();
            }
            else
            {
                FrameImage.sprite = FrameEmptySprite;
                IconImage.color = UnityUtil.TransparentWhite;
                AmountBadge.gameObject.SetActive(false);
            }
        }

        public void UseItem()
        {
            
        }

        #region Drag and drop

        public void OnPointerDown(PointerEventData eventData)
        {
            var dragHelper = GetService<ClickDragHelper>();
            if (dragHelper.IsDragging)
            {
                // Drop
                if (!Container.IsDroppable)
                    return;
                if (dragHelper.DragDummy is not ItemDragDummy dummy)
                    return;

                if (dummy.Slot != this)
                {
                    Container.StackOrSwap(dummy.Slot, this);
                }
                dragHelper.EndDrag();
            }
            else if (Item != null && Container.IsDraggable)
            {
                // Drag
                var gameUI = GetService<GameUI>();
                var dummy = gameUI.Pooler.Spawn<ItemDragDummy>(DragDummyPrefab);
                dummy.Setup(Region);
                dummy.SetData(gameUI.Pooler, this);
            
                // Offset
                // var cursorPos = GetSystem<InputSystem>().Actions.Game.CursorPosition.ReadValue<Vector2>();
                // var cursorWorldPos = gameUI.UICam.ScreenToWorldPoint(cursorPos);
                // var offset = _rectTrans.position - cursorWorldPos;
                // offset.z = 0f;

                dragHelper.BeginDrag(dummy);
            }
        }

        #endregion

    }
}