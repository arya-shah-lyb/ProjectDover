using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectDover.tests
{
    class InventoryShould
    {
        private readonly Inventory _inventory;

        public InventoryShould()
        {
            _inventory = new Inventory();
        }

        [Fact]
        public void Inventory_ByDefault_ShouldBeNamed() {
            Assert.Equal("RoomItems", _inventory.Name);
        }

        [Fact]
        public void Inventory_ByDefault_ShouldEmptyItemList()
        {
            Assert.Empty(_inventory.Items);
        }

        //public void Inventory_AddItem_ShouldEmptyItemList()
        //{
        //    Assert.Empty(_inventory.Items);
        //}

    }
}
