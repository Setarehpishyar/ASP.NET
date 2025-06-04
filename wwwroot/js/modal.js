document.addEventListener("DOMContentLoaded", () => {
    // === Add Project Modal ===
    const addProjectModal = document.getElementById("projectModal");
    const addProjectBtn = document.getElementById("addProjectBtn");
    const closeProjectModalBtn = document.getElementById("closeModal");
    const cancelProjectBtn = document.getElementById("cancelBtn");

    if (addProjectModal) addProjectModal.style.display = "none";

    addProjectBtn?.addEventListener("click", () => {
        if (addProjectModal) addProjectModal.style.display = "flex";
    });

    closeProjectModalBtn?.addEventListener("click", () => {
        if (addProjectModal) addProjectModal.style.display = "none";
    });

    cancelProjectBtn?.addEventListener("click", () => {
        if (addProjectModal) addProjectModal.style.display = "none";
    });

    // فایل و پیش‌نمایش تصویر پروژه
    const projectImageInput = document.getElementById("projectImageInput");
    const projectImagePreview = document.getElementById("projectImagePreview");
    const uploadCircle = document.querySelector(".upload-circle");

    if (uploadCircle && projectImageInput) {
        uploadCircle.addEventListener("click", () => {
            projectImageInput.click();
        });
    }

    projectImageInput?.addEventListener("change", function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                projectImagePreview.src = e.target.result;
                projectImagePreview.style.display = "block";
            };
            reader.readAsDataURL(file);
        }
    });

    // === Add Member Modal ===
    const memberModal = document.getElementById("memberModal");
    const addMemberBtn = document.getElementById("addMemberBtn");
    const closeMemberModalBtn = document.getElementById("closeMemberModal");

    if (memberModal) memberModal.style.display = "none";

    addMemberBtn?.addEventListener("click", () => {
        if (memberModal) memberModal.style.display = "flex";
    });

    closeMemberModalBtn?.addEventListener("click", () => {
        if (memberModal) memberModal.style.display = "none";
    });

    // فایل و پیش‌نمایش تصویر عضو
    const projectImageInputMember = document.getElementById("projectImageInputMember");
    const projectImagePreviewMember = document.getElementById("projectImagePreviewMember");
    const uploadCircleMember = document.querySelector(".upload-circle1");

    if (uploadCircleMember && projectImageInputMember) {
        uploadCircleMember.addEventListener("click", () => {
            projectImageInputMember.click();
        });
    }

    projectImageInputMember?.addEventListener("change", function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                projectImagePreviewMember.src = e.target.result;
                projectImagePreviewMember.style.display = "block";
            };
            reader.readAsDataURL(file);
        }
    });

    // === Edit Project Modal ===
    const editModal = document.getElementById("editProjectModal");
    const editImage = document.getElementById("editProjectImage");
    const closeEditBtn = document.getElementById("closeEditModal");

    closeEditBtn?.addEventListener("click", () => {
        if (editModal) editModal.style.display = "none";
    });

    const editLinks = document.querySelectorAll(".dropdown-menu .edit");

    editLinks.forEach(link => {
        link.addEventListener("click", (e) => {
            e.preventDefault();

            const projectCard = link.closest(".project");
            const projectImg = projectCard.querySelector(".project-icon");

            if (projectImg && editImage) {
                editImage.src = projectImg.src;
                editImage.style.display = "block";
            }

            if (editModal) {
                editModal.style.display = "flex";
            }

            const projectTitle = projectCard.querySelector(".project-title-text h4")?.textContent;
            const clientName = projectCard.querySelector(".company")?.textContent;
            const desc = projectCard.querySelector(".desc")?.textContent;

            document.getElementById("editProjectTitle").value = projectTitle || "";
            document.getElementById("editClientName").value = clientName || "";
            document.getElementById("editProjectDesc").value = desc || "";
        });
    });

    // بستن مودال‌ها با کلیک روی فضای بیرون مودال
    window.addEventListener("click", (e) => {
        if (e.target === addProjectModal) addProjectModal.style.display = "none";
        if (e.target === memberModal) memberModal.style.display = "none";
        if (e.target === editModal) editModal.style.display = "none";
    });
});
