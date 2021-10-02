import * as React from "react";
import { Flex } from "@chakra-ui/layout";
import { Text } from "@chakra-ui/react";
import { CopyButton } from "./CopyButton";

export function InfoText() {
    return (
        <Flex color="white" pb={8}>
            <Text
                color="white"
                fontSize="3xl"
                fontWeight="semibold"
                textShadow="0 1px 3px rgba(0,0,0,0.3)"
            >
                mc.noob-box.net
            </Text>
            <CopyButton value="mc.noob-box.net" />
        </Flex>
    );
}
