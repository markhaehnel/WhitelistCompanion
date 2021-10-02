import React from "react";
import ReactDOM from "react-dom";
import "simplebar/dist/simplebar.min.css";
import App from "./App";
import { ChakraProvider, ColorModeScript, Flex } from "@chakra-ui/react";
import theme from "./theme";

ReactDOM.render(
    <React.StrictMode>
        <ColorModeScript initialColorMode={theme.config.initialColorMode} />
        <ChakraProvider theme={theme}>
            <Flex
                direction="column"
                alignItems="center"
                justify="center"
                minH="100vh"
                py={8}
            >
                <App />
            </Flex>
        </ChakraProvider>
    </React.StrictMode>,
    document.getElementById("root")
);
