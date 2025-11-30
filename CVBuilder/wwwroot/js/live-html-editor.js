function liveEditor() {
    return {
        html: `
        <!DOCTYPE html>
        <html>
        <head>
        <meta charset="utf-8"/>
        </head>
        <body>
           <h1>Hello</h1>
        </body>
</html>`,
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
