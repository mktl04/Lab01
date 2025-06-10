
$(document).ready(function () {
    showQuantityCart();
});
let showQuantityCart = () => {
    $.ajax({
        url: "/Customer/Cart/GetQuantityOfCart",
        success: function (data) {
            $(".showcart").text(data.quanty);
        }
    });
}
//xử lý sự kiện click cho các liên kết [add to cart]
$(document).on("click", ".addtocart", function (evt) {
    evt.preventDefault();
    let id = $(this).attr("data-productId");
    $.ajax({
        url: "/Customer/Cart/AddToCartAPI",
        data: { "productId": id },
        success: function (data) {
            Swal.fire({
                title: "Product added to cart",
                text: "You clicked the button!",
                icon: "success"
            });
            showQuantityCart();
        }
    });
})