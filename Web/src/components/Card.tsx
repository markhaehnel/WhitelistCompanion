import { useColorModeValue } from "@chakra-ui/color-mode";
import { Box } from "@chakra-ui/layout";
import * as React from "react";

const Card: React.FC<{ error?: boolean }> = ({ error = false, children }) => {
    const bgColor = useColorModeValue("white", "gray.800");
    const bgColorWithError = error ? "red.600" : bgColor;

    const textColor = useColorModeValue("black", "white");
    const textColorWithError = error ? "white" : textColor;

    return (
        <Box
            bgColor={bgColorWithError}
            textColor={textColorWithError}
            rounded={{ base: "none", md: "lg" }}
            shadow="lg"
            overflow="auto"
        >
            {children}
        </Box>
    );
};

export { Card };
