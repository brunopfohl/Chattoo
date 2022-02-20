import { LocalizationProvider } from "@mui/lab";
import styled from "@emotion/styled";
import { Box, ThemeProvider } from "@mui/material";
import { FC } from "react";
import theme from "../../../styles/theme";
import DateAdapter from '@mui/lab/AdapterMoment';

/** Styl pozadí */
const Gradient = styled.div`
    background: #F0F2F5;
    height: 100vh;
`;

/**
 * Komponenta rozložení.
 */
const Layout: FC = ({ children }) => (
    <ThemeProvider theme={theme}>
        <LocalizationProvider dateAdapter={DateAdapter}>
            {/* Obsah z children komponenty */}
            <Gradient>
                <Box sx={{ pt: 5, height: "100%", boxSizing: "border-box" }}>
                    {children}
                </Box>
            </Gradient>
        </LocalizationProvider>
    </ThemeProvider>
);

Layout.displayName = "LayoutComponent";
export default Layout;