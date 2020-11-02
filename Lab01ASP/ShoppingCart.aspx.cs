using Lab01ASP.Logic;
using Lab01ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Collections;
using System.Web.ModelBinding;

namespace Lab01ASP
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ShippingCartAction usersShoppingCart = new ShippingCartAction())
            {
                decimal cartTotal = 0;
                cartTotal = usersShoppingCart.GetTotal();
                if(cartTotal > 0)
                {
                    lblTotal.Text = String.Format("{0:c}", cartTotal);
                }
                else
                {
                    LabelTotalText.Text = "";
                    lblTotal.Text = "";
                    ShoppingCartTitle.InnerText = "Shopping Cart is Empty";
                    UpdateBtn.Visible = false;
                }
            }

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
       public List<CartItem> GetShoppingCartItems()
        {
            ShippingCartAction actions = new ShippingCartAction();
            return actions.GetCastItems();
        }
        public List<CartItem> UpdateCartItems()
        {
            using (ShippingCartAction usersShoppingCart = new ShippingCartAction())
            {
                String cartId = usersShoppingCart.GetCartId();
                ShippingCartAction.ShoppingCartUpdates[] cartUpdates = new
                    ShippingCartAction.ShoppingCartUpdates[CartList.Rows.Count];
                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove =(CheckBox) CartList.Rows[i].FindControl( "Remove");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl( "PurchaseQuantity");
                    cartUpdates[i].PurchaseQuantity =
                    Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblTotal.Text = String.Format( "{0:c}", usersShoppingCart.GetTotal());
                return usersShoppingCart.GetCartItems(); 
                
                }
             }

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values =  new OrderedDictionary();
            foreach (  DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible) 
                {
                    // Extract values from the cell.
                    cell.ContainingField.ExtractValuesFromCell(values, cell,
                    row.RowState, true); 
                }
            }
            return values; 
}
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
        }
    }
}