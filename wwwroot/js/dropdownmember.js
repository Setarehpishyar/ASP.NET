document.addEventListener("DOMContentLoaded", () => {
    console.log("Dropdown script is running!");

    // === باز و بسته کردن منوی کشویی برای هر عضو تیم ===
    document.querySelectorAll(".dropdown-toggle").forEach(toggle => {
        toggle.addEventListener("click", (e) => {
            e.stopPropagation();

            const parent = toggle.closest(".member-actions");

            if (!parent) {
                console.error("محدوده منوی کشویی (.member-actions) پیدا نشد!");
                return;
            }

            // بستن همه منوهای باز قبل از باز کردن مورد جدید
            document.querySelectorAll(".member-actions").forEach(el => el.classList.remove("open"));

            // باز/بسته کردن همین مورد
            parent.classList.toggle("open");
        });
    });

    // بستن منوی کشویی هنگام کلیک خارج از آن
    document.addEventListener("click", () => {
        document.querySelectorAll(".member-actions").forEach(el => el.classList.remove("open"));
    });

    // === باز کردن مودال ویرایش با اطلاعات عضو ===
    document.querySelectorAll(".dropdown-menu .edit").forEach(button => {
        button.addEventListener("click", (e) => {
            e.preventDefault();
            e.stopPropagation(); // برای اینکه dropdown بسته نشود تا زمانی که modal باز شود

            const memberCard = button.closest(".member");
            if (!memberCard) return;

            // گرفتن اطلاعات از کارت عضو
            const fullName = memberCard.querySelector("h4")?.innerText?.trim().split(" ") || [];
            const email = memberCard.querySelectorAll("p")[0]?.innerText?.trim();
            const phone = memberCard.querySelectorAll("p")[1]?.innerText?.trim();
            const jobTitle = memberCard.querySelector("small")?.innerText?.trim();
            const imageSrc = memberCard.querySelector("img.preview-image2")?.src;

            // پر کردن فرم مودال ویرایش
            const modal = document.getElementById("editmemberModal");
            modal.querySelector("input[name='firstName']").value = fullName[0] || '';
            modal.querySelector("input[name='lastName']").value = fullName.slice(1).join(" ") || '';
            modal.querySelector("input[name='email']").value = email || '';
            modal.querySelector("input[name='phone']").value = phone || '';
            modal.querySelector("input[name='jobTitle']").value = jobTitle || '';
            modal.querySelector("#editProjectImage").src = imageSrc || '';

            // نمایش مودال
            modal.style.display = "block";
        });
    });
});
