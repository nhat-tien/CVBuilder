function liveEditor() {
    return {
        html: `
        <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Professional CV</title>
    <style>
        /* ========== GENERAL ========== */
body {
    font-family: "Inter", Arial, sans-serif;
    background: #f2f4f7;
    margin: 0;
    line-height: 1.6;
}

html, body {
    margin: 0;
    padding: 0;
    width: 210mm;
    min-height: 297mm;
    overflow-x: hidden;
    background: #f2f4f7;
}

.cv-wrapper {
    margin: auto;
    background: white;
    display: grid;
    grid-template-columns: 1fr 2fr;
    box-shadow: 0 8px 30px rgba(0,0,0,0.08);
    overflow: hidden;
    aspect-ratio: 210 / 297;
    width: 210mm;
    height: auto;
    min-height: 297mm;
}
/* ========== SIDEBAR ========== */
.sidebar {
    background: #1f2937;
    color: white;
    padding: 30px;
}

.profile h1 {
    font-size: 28px;
    margin-bottom: 5px;
}

.role {
    color: #9ca3af;
    margin-bottom: 25px;
}

.sidebar-section h2 {
    font-size: 16px;
    margin-bottom: 10px;
    border-bottom: 1px solid #374151;
    padding-bottom: 5px;
}

.sidebar-section ul {
    padding-left: 20px;
    margin: 0;
}

.sidebar-section ul li {
    margin-bottom: 8px;
    color: #d1d5db;
    font-size: 14px;
}

/* ========== MAIN CONTENT ========== */
.content {
    padding: 40px 50px;
}

.content-section {
    margin-bottom: 35px;
}

.content-section h2 {
    font-size: 20px;
    margin-bottom: 10px;
    color: #111827;
    border-bottom: 2px solid #e5e7eb;
    padding-bottom: 5px;
}

/* Job Section */
.job {
    margin-bottom: 20px;
}

.job-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.job-header h3 {
    margin: 0;
    font-size: 18px;
}

.date {
    color: #6b7280;
    font-size: 14px;
}

.job ul {
    padding-left: 20px;
    margin: 10px 0 0 0;
}

.job ul li {
    margin-bottom: 8px;
}

/* Print-friendly */
@media print {
    body {
        padding: 0;
        background: white;
    }
    .cv-wrapper {
        box-shadow: none;
        border-radius: 0;
    }
}

    </style>
</head>
<body>

    <div class="cv-wrapper">
        <div class="sidebar">
            <div class="profile">
                <h1>John Doe</h1>
                <p class="role">Senior Software Engineer</p>
            </div>

            <div class="sidebar-section">
                <h2>Contact</h2>
                <ul>
                    <li>Email: john@example.com</li>
                    <li>Phone: +1 234 567 890</li>
                    <li>Website: johndoe.dev</li>
                    <li>Address: New York, USA</li>
                </ul>
            </div>

            <div class="sidebar-section">
                <h2>Skills</h2>
                <ul>
                    <li>JavaScript / TypeScript</li>
                    <li>React / Node.js</li>
                    <li>Microservices Architecture</li>
                    <li>SQL / NoSQL</li>
                    <li>Docker / Kubernetes</li>
                </ul>
            </div>

            <div class="sidebar-section">
                <h2>Languages</h2>
                <ul>
                    <li>English (Fluent)</li>
                    <li>Spanish (Intermediate)</li>
                </ul>
            </div>
        </div>

        <div class="content">
            <section class="content-section">
                <h2>Profile</h2>
                <p>
                    Senior software engineer with 7+ years of experience designing scalable
                    backend systems, leading engineering teams, and delivering high-impact
                    products. Strong background in cloud-native applications and distributed
                    system design.
                </p>
            </section>

            <section class="content-section">
                <h2>Experience</h2>

                <div class="job">
                    <div class="job-header">
                        <h3>Lead Software Engineer – ABC Tech</h3>
                        <span class="date">2021 – Present</span>
                    </div>
                    <ul>
                        <li>Designed and built microservice architecture serving 2M+ users.</li>
                        <li>Led a team of 6 engineers, improving delivery speed by 40%.</li>
                        <li>Delivered a real-time data pipeline reducing latency from 600ms → 80ms.</li>
                    </ul>
                </div>

                <div class="job">
                    <div class="job-header">
                        <h3>Software Engineer – XYZ Corp</h3>
                        <span class="date">2018 – 2021</span>
                    </div>
                    <ul>
                        <li>Developed internal automation tools saving 200+ hours/month.</li>
                        <li>Maintained backend services with 99.98% uptime.</li>
                    </ul>
                </div>
            </section>

            <section class="content-section">
                <h2>Education</h2>
                <p><strong>B.S. in Computer Science</strong> – University of Technology (2014 – 2018)</p>
            </section>
        </div>
    </div>
</body>
</html>


        `,
        auto: true,
        timer: null,

        changed() {
            if (this.auto) {
                clearTimeout(this.timer);
                this.timer = setTimeout(() => this.run(), 300);
            }
        },

        run() {
            const blob = new Blob([this.html], { type: "text/html" });
            const url = URL.createObjectURL(blob);

            this.$refs.frame.src = url;
        },

        init() {
            this.run();
        }
    }
}
