document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 150;

    document.querySelectorAll('[data-modal="true"]').forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);
            if (modal) modal.style.display = 'flex';
        });
    });

    document.querySelectorAll('[data-close="true"]').forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal');
            if (modal) modal.style.display = 'none';
        });
    });

    document.querySelectorAll('.upload-box3').forEach(previewer => {
        const trigger = previewer.querySelector('#uploadTrigger');
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('img');
        const icon = trigger.querySelector('i');

        if (trigger && fileInput && imagePreview) {
            trigger.addEventListener('click', () => fileInput.click());

            fileInput.addEventListener('change', async (event) => {
                const file = event.target.files[0];
                if (file) {
                    await processImage(file, imagePreview, previewer, previewSize);
                    if (icon) icon.style.display = 'none';
                    imagePreview.style.display = 'block';
                }
            });
        }
    });

    async function loadImage(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onerror = () => reject("File reader error");
            reader.onload = (e) => {
                const img = new Image();
                img.onerror = () => reject("Upload error");
                img.onload = () => resolve(img);
                img.src = e.target.result;
            };
            reader.readAsDataURL(file);
        });
    }

    async function processImage(file, imagePreview, previewer, previewSize = 150) {
        try {
            const img = await loadImage(file);
            const canvas = document.createElement('canvas');
            canvas.width = previewSize;
            canvas.height = previewSize;

            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0, previewSize, previewSize);
            imagePreview.src = canvas.toDataURL('image/jpeg');
            previewer.classList.add('selected');
        } catch (err) {
            console.error("Image error:", err);
        }
    }

    document.querySelectorAll('form').forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            clearErrorMessages(form);

            const formData = new FormData(form);
            try {
                const res = await fetch(form.action, {
                    method: 'POST',
                    body: formData
                });

                if (res.ok) {
                    const modal = form.closest('.modal');
                    if (modal) modal.style.display = 'none';
                    window.location.reload();
                } else if (res.status === 400) {
                    const data = await res.json();
                    showValidationErrors(form, data.errors);
                }
            } catch (err) {
                console.log("Error:", err);
            }
        });
    });

    function clearErrorMessages(form) {
        form.querySelectorAll('[data-val="true"]').forEach(input => {
            input.classList.remove('input-validation-error');
        });

        form.querySelectorAll('[data-valmsg-for]').forEach(span => {
            span.innerText = '';
            span.classList.remove('field-validation-error');
        });
    }

    function showValidationErrors(form, errors) {
        Object.keys(errors).forEach(key => {
            const input = form.querySelector(`[name="${key}"]`);
            const span = form.querySelector(`[data-valmsg-for="${key}"]`);
            if (input) input.classList.add('input-validation-error');
            if (span) {
                span.innerText = errors[key].join('\n');
                span.classList.add('field-validation-error');
            }
        });
    }

    document.querySelectorAll("select").forEach(select => {
        select.addEventListener("change", () => {
            document.querySelector("[name='Date']").value =
                document.getElementById("dobYear").value + "-" +
                document.getElementById("dobMonth").value + "-" +
                document.getElementById("dobDay").value;
        });
    });

    const mainForm = document.querySelector("form");
    if (mainForm) {
        mainForm.addEventListener("submit", function (e) {
            const status = document.querySelector("select[name='Status']").value;
            if (!status) {
                alert("Please select the status.");
                e.preventDefault();
            }
        });
    }

    document.querySelectorAll(".edit-project-btn").forEach(button => {
        button.addEventListener("click", function () {
            const projectId = this.getAttribute("data-project-id");
            fetch(`/projects/getproject?id=${projectId}`)
                .then(res => res.json())
                .then(data => {
                    document.getElementById("projectId").value = data.id;
                    document.getElementById("editTitle").value = data.title;
                    document.getElementById("editCompany").value = data.company;
                    document.getElementById("editDescription").value = data.description;
                    document.getElementById("editStartDate").value = data.startDate;
                    document.getElementById("editEndDate").value = data.endDate;
                    document.getElementById("editBudget").value = data.budget;
                    document.getElementById("editMembers").value = data.members;
                    document.getElementById("editStatus").value = data.status;

                    document.getElementById('editForm').setAttribute('action', `/projects/edit/${data.id}`);
                    const imagePreview = document.getElementById("EditProjectImagePreview");
                    if (imagePreview) {
                        imagePreview.src = `/uploads/${data.imagePath || "default.svg"}`;
                        imagePreview.style.display = "block";
                    }

                    document.getElementById("EditProjectModal").style.display = "block";
                });
        });
    });

    const editForm = document.getElementById('editForm');
    if (editForm) {
        editForm.addEventListener('submit', function (e) {
            e.preventDefault();

            const formData = new FormData(editForm);
            fetch(editForm.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.redirected) {
                    window.location.href = response.url;
                } else if (response.ok) {
                    document.getElementById("EditProjectModal").style.display = "none";
                    window.location.reload();
                } else {
                    return response.json().then(data => {
                        if (data.errors) showValidationErrors(editForm, data.errors);
                    });
                }
            }).catch(error => {
                console.error("Editing error:", error);
            });
        });
    }

    const closeEditModalBtn = document.querySelector('#EditProjectModal .close');
    if (closeEditModalBtn) {
        closeEditModalBtn.addEventListener('click', () => {
            document.getElementById('EditProjectModal').style.display = 'none';
        });
    }

    document.querySelectorAll('.dropdown-toggle').forEach(button => {
        button.addEventListener('click', e => {
            e.stopPropagation();
            const dropdown = button.nextElementSibling;
            dropdown.classList.toggle('show');
        });
    });

    document.addEventListener('click', () => {
        document.querySelectorAll('.dropdown-menu.show').forEach(menu => {
            menu.classList.remove('show');
        });
    });

    document.getElementById('themeToggle').addEventListener('click', () => {
        document.body.classList.toggle('dark-mode');
        localStorage.setItem("theme", document.body.classList.contains('dark-mode') ? "dark" : "light");
    });

    window.onload = () => {
        if (localStorage.getItem("theme") === "dark") {
            document.body.classList.add("dark-mode");
        }
    };

    document.getElementById("notificationToggle").addEventListener("click", () => {
        fetch("/api/notifications")
            .then(res => res.json())
            .then(data => {
                const list = document.getElementById("notificationList");
                list.innerHTML = "";
                data.forEach(n => {
                    const div = document.createElement("div");
                    div.innerHTML = `<strong>${n.title}</strong><br/>${n.message}`;
                    list.appendChild(div);
                });
            });
    });


});




