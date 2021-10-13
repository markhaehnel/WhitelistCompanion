import * as React from "react";
import { Flex } from "@chakra-ui/layout";
import { Spinner, Text } from "@chakra-ui/react";
import { CopyButton } from "./CopyButton";
import { useQuery } from "react-query";
import { fetchUiConfig } from "../api";

export function InfoText() {
    const { data } = useQuery("uiConfig", fetchUiConfig, {
        retry: 1,
        refetchOnWindowFocus: false,
        refetchInterval: false,
    });

    return (
        <Flex color="white" pb={8}>
            <Text
                color="white"
                fontSize="3xl"
                fontWeight="semibold"
                textShadow="0 1px 3px rgba(0,0,0,0.3)"
            >
                {data?.data?.serverAddress}
            </Text>
            <CopyButton value="mc.noob-box.net" />
        </Flex>
    );
}
