function liveEditor() {
    return {
        html: `
 <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>{{name}} – CV</title>

    <style>
        /* --- your CSS unchanged --- */
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
            min-height: 297mm;
        }
        .sidebar {
            background: #1f2937;
            color: white;
            padding: 30px;
        }
        .profile h1 { font-size: 28px; margin-bottom: 5px; }
        .role { color: #9ca3af; margin-bottom: 25px; }

        .sidebar-section h2 {
            font-size: 16px;
            margin-bottom: 10px;
            border-bottom: 1px solid #374151;
            padding-bottom: 5px;
        }

        .sidebar-section ul { padding-left: 20px; margin: 0; }
        .sidebar-section ul li { margin-bottom: 8px; color: #d1d5db; font-size: 14px; }

        .content { padding: 40px 50px; }
        .content-section {
            text-align: justify;
            margin-bottom: 35px;
         }

        .content-section h2 {
            font-size: 20px;
            margin-bottom: 10px;
            color: #111827;
            border-bottom: 2px solid #e5e7eb;
            padding-bottom: 5px;
        }

        .job { margin-bottom: 20px; }
        .job-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        .job-header h3 { margin: 0; font-size: 18px; }
        .date { color: #6b7280; font-size: 14px; }

        .job ul { padding-left: 20px; margin: 10px 0 0 0; }
        .job ul li { margin-bottom: 8px; }
    </style>
</head>

<body>
    <div class="cv-wrapper">

        <!-- Sidebar -->
        <div class="sidebar">
            <div class="profile">
                <h1>{{name}}</h1>
                <p class="role">{{role}}</p>
            </div>

            <div class="sidebar-section">
                <h2>Contact</h2>
                <ul>
                    <li>Email: {{contact.email}}</li>
                    <li>Phone: {{contact.phone}}</li>
                    <li>Website: {{contact.website}}</li>
                    <li>Address: {{contact.address}}</li>
                </ul>
            </div>

            {{#each side-sections}}
            <div class="sidebar-section">
                <h2>{{title}}</h2>
                {{#each subsections}}
                {{#if items}}
                <ul>
                    {{#each items}}
                        <li>{{this}}</li>
                    {{/each}}
                </ul>
                {{/if}}
                {{/each}}
            </div>
            {{/each}}
        </div>

        <!-- Main Content -->
        <div class="content">
            {{#each sections}}
            <section class="content-section">
                <h2>{{title}}</h2>
                {{#each subsections}}
                {{#if text}}
                    <p>{{text}}</p>
                {{/if}}
                {{#if title}}
                <div class="job">
                    <div class="job-header">
                        <h3>{{title}}</h3>
                        <span class="date">{{date}}</span>
                    </div>
                    <ul>
                        {{#each items}}
                            <li>{{this}}</li>
                        {{/each}}
                    </ul>
                </div>
                {{/if}}
                {{/each}}
            </section>
            {{/each}}
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
                this.timer = setTimeout(() => this.run(), 3000);
            }
        },

        run() {
            const html = htmlTemplateWithDefaultData(this.html);
            const blob = new Blob([html], { type: "text/html" });
            const url = URL.createObjectURL(blob);

            this.$refs.frame.src = url;
        },

        download() {
            html2canvas(this.$refs.frame.contentDocument.body).then(canvas => {
                const img = canvas.toDataURL("image/png");

                // Show or download
                const a = document.createElement("a");
                a.href = img;
                a.download = "capture.png";
                a.click();
            });
        },
        prepareAndSubmit(e) {
            e.preventDefault();

            html2canvas(this.$refs.frame.contentDocument.body).then(canvas => {
                const img = canvas.toDataURL("image/png");

                
                this.$refs.previewField.value = img;

 
                e.target.submit();
            });
        },

        init() {
            this.run();
        }
    }
}
function htmlTemplateWithDefaultData(template) {
    let templater = Handlebars.compile(template);
    return templater({
        "name": "John Doe",
        "role": "Senior Software Engineer",
        "contact": {
            "email": "john@example.com",
            "phone": "+1 234 567 890",
            "website": "johndoe.dev",
            "address": "New York, USA"
        },


        "side-sections": [
          {
            "title": "Skills",
            "subsections": [
                {
                    "items": [
            "JavaScript / TypeScript",
            "React / Node.js",
            "Microservices Architecture",
            "SQL / NoSQL",
            "Docker / Kubernetes"
                    ]
                }
            ]
          },
          {
            "title": "Languages",
            "subsections": [
                {
                    "items": [
            "English (Fluent)",
            "Spanish (Intermediate)"
                    ]
                }
            ]
          }
        ],
        "sections": [
        {
                "title": "Profile",
                "subsections": [
                    {
                        text: `Senior software engineer with 7+ years of experience designing scalable
                    backend systems, leading engineering teams, and delivering high - impact
                    products. Strong background in cloud - native applications and distributed
                    system design.`
                    }
                ]
            },
        {
                "title": "Experiences",
                "subsections": [
        {
                "title": "Lead Software Engineer - ABC Tech",
                "date": "2021 - Present",
                items: [
                    "Designed and built microservice architecture serving 2M+ users.",
                    "Led a team of 6 engineers...",
                    "Delivered a real-time data pipeline..."
                        ]
                    },
        {
                "title": "Software Engineer - XYZ Corp",
                "date": "2018 - 2021",
                    items: [
                    "Developed internal automation tools...",
                    "Maintained backend services with 99.98% uptime."
                    ]
        },
                ]
        },
        {
                "title": "Education",
                "subsections": [
                {
                    text:  "B.S. in Computer Science - University of Technology (2014 - 2018)"
                }
                ]
        }
        ]
    })
}