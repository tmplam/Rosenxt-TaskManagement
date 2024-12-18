/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        primary: "#005cbb",
        secondary: "#343dff",
        danger: "#b91c1c",
      },
    },
  },
  plugins: [],
};
