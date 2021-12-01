import styled from "@emotion/styled";
import { ThemeProvider } from "@mui/material";
import theme from "../../../styles/theme";

const Container = styled.div`
    background: linear-gradient(149deg, rgba(2,184,147,1) 0%, rgba(39,160,221,1) 50%, rgba(194,39,194,1) 100%);
    height: 100vh;
    padding: 5vh 10vw 10vh 5vw;
`;

const Layout: React.FC<any> = ({ children }) => {
    return (
        <ThemeProvider theme={theme}>
            <Container>
                {children}
            </Container>
        </ThemeProvider>
    );
};

export default Layout;