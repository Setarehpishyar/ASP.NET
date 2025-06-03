// notification.js یا در اسکریپت جدا

// 📌 دریافت المنت‌ها
const notificationBox = document.getElementById("notificationBox");
const notificationList = document.getElementById("notificationList");
const notificationBadge = document.querySelector(".notification-badge");
const notificationToggle = document.querySelector(".notification-icon");

let notifications = [
    {
        id: Date.now(),
        image: "images/img1.svg",
        message: "ASP.NET Web App added",
        time: "1 min ago"
    }
];

// 📌 رندر اعلان‌ها
function renderNotifications() {
    notificationList.innerHTML = "";

    notifications.forEach(notif => {
        const item = document.createElement("div");
        item.className = "notification-item";
        item.innerHTML = `
      <img src="${notif.image}" alt="icon">
      <div class="notification-text">
        <p>${notif.message}</p>
        <span>${notif.time}</span>
      </div>
      <button class="notification-close-btn" onclick="removeNotification(${notif.id})">&times;</button>
    `;
        notificationList.appendChild(item);
    });

    notificationBadge.style.display = notifications.length ? "block" : "none";
}

// 📌 حذف اعلان
function removeNotification(id) {
    notifications = notifications.filter(n => n.id !== id);
    renderNotifications();
}

// 📌 افزودن اعلان
function addNotification(message, time, image) {
    notifications.unshift({
        id: Date.now(),
        message,
        time,
        image
    });
    renderNotifications();
}

// 📌 باز/بستن جعبه اعلان
if (notificationToggle) {
    notificationToggle.addEventListener("click", () => {
        notificationBox.style.display =
            notificationBox.style.display === "block" ? "none" : "block";
        renderNotifications();
    });
}

// 📌 افزودن اعلان هنگام افزودن پروژه
document.addEventListener("DOMContentLoaded", () => {
    const createBtn = document.getElementById("addBtn");

    if (createBtn) {
        createBtn.addEventListener("click", () => {
            const titleInput = document.getElementById("projectTitle");
            const projectImage = document.getElementById("projectImagePreview");

            const title = titleInput?.value.trim() || "New Project";
            const imageSrc =
                projectImage?.src && projectImage.style.display !== "none"
                    ? projectImage.src
                    : "images/img1.svg";

            addNotification(`Project "${title}" added successfully`, "just now", imageSrc);

            // اختیاری: بستن مدال پس از افزودن
            document.getElementById("projectModal").style.display = "none";
        });
    }
});