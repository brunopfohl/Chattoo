import { createTheme } from "@mui/material/styles";

const theme = createTheme({
    palette: {
        primary: {
            main: "rgba(39,160,221,1)",
        },
        secondary: {
            main: "rgba(194,39,194,1)"
        },
        success: {
            main: "rgba(2,184,147,1)"
        },
        error: {
            main: "#973831"
        }
    }
});

export default theme;