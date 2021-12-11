import styled from "@emotion/styled";
import { Container, ThemeProvider } from "@mui/material";
import { FC } from "react";
import theme from "../../../styles/theme";

/** Styl pozadí */
const Gradient = styled.div`
    background: linear-gradient(149deg, rgba(2,184,147,1) 0%, rgba(39,160,221,1) 50%, rgba(194,39,194,1) 100%);
    height: 100vh;
    width: 100vw;
`;

/**
 * Komponenta rozložení.
 */
const Layout: FC = ({ children }) => (
    <ThemeProvider theme={theme}>
        {/* Obsah z children komponenty */}
        <Gradient>
            <Container maxWidth="xl" sx={{ pt: 10 }}>
                {children}
            </Container>
        </Gradient>
    </ThemeProvider>
);

Layout.displayName = "LayoutComponent";
export default Layout;