# UART Samples

## MK-3: Advanced UART

```c
#define PACKET_RX_MAX 255
static uint8_t uart4_rx_dma_buf[PACKET_RX_MAX];
static uint8_t uart4_msg_buf[PACKET_RX_MAX + 1];

static volatile uint16_t _uartMsgLen = 0;
static volatile uint8_t _uartMsgReady = 0;

void HAL_UARTEx_RxEventCallback(UART_HandleTypeDef *huart, uint16_t size)
{
  if (huart->Instance == UART4)
  {
    // Handle Packet Messages ======
    bool isActive = PacketRxIsr();
    if (isActive)
      isActive = PacketTxIsr();

    // Check timer interrupt (~4.1ms) so we can trigger the
    // PacketBspRxTimerUpdate() and PacketBspBootloadTmrUpdate();
    if (!isActive)
    {
    }

    // Generic Testing ==============
    if (size > PACKET_RX_MAX)
      size = PACKET_RX_MAX;

    memcpy(uart4_msg_buf, uart4_rx_dma_buf, size);
    uart4_msg_buf[size] = '\0';
    _uartMsgLen = size;
    _uartMsgReady = 1;

    // Restart variable-length reception
    HAL_UARTEx_ReceiveToIdle_IT(&huart4, uart4_rx_dma_buf, PACKET_RX_MAX);
  }
}

int main()
{
  // ...

  // Send initial HELLO
  const char *ready = "UART Ready\0";
  HAL_UART_Transmit(&huart4, (uint8_t *)ready, strlen(ready), HAL_MAX_DELAY);

  // Receive variable length data until UART idle line or 255 bytes received
  HAL_UARTEx_ReceiveToIdle_IT(&huart4, uart4_rx_dma_buf, PACKET_RX_MAX);

  while (1)
  {
    // Check if we got anything and respond
    if (_uartMsgReady)
    {
      _uartMsgReady = 0;
      HAL_UART_Transmit(&huart4, (uint8_t *)"Received: ", 10, HAL_MAX_DELAY);
      HAL_UART_Transmit(&huart4, uart4_msg_buf, _uartMsgLen, HAL_MAX_DELAY);
      HAL_UART_Transmit(&huart4, (uint8_t *)"END\0: ", 4, HAL_MAX_DELAY);
    }
  }
}

```

## MK-2: Simple UART

Basic UART Receive/Transmit working sample

```c
static uint8_t _uartRxByte;
static volatile uint8_t _uartByteReady = 0;

static void UART4_SendString(const char *msg)
{
  HAL_UART_Transmit(&huart4, (uint8_t *)msg, strlen(msg), HAL_MAX_DELAY);
}

// Regular uart ready
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
  if (huart->Instance == UART4)
  {
    _uartByteReady = 1;

    // Restart receive interrupt for next byte
    HAL_UART_Receive_IT(&huart4, &_uartRxByte, 1);
  }
}

void main()
{
  // ...

  UART4_SendString("UART4 ready\r\n");
  HAL_UART_Receive_IT(&huart4, &_uartRxByte, 1);

  while (1)
  {
    if (_uartByteReady)
    {
      _uartByteReady = 0;
      UART4_SendString("HEY!\0");
      HAL_UART_Transmit(&huart4, &_uartRxByte, 1, HAL_MAX_DELAY);
      UART4_SendString("END\0");
    }
  }
}
```

## MK1: Dirty Tests (Don't work)

```c
void TestUart()
{
  // TESTING START
  uint32_t isrFlags = 0L;
  uint8_t _txData[] = "Hello, World!\0";

  // Reflect what is sent to us
  UART_HandleTypeDef *huart = &huart4;
  isrFlags = huart->Instance->ISR;
  while ((isrFlags & USART_ISR_RXNE_RXFNE) != 0)
  {
    // Automatically clears the RXNE (receive not empty) interrupt flag
    // Applying 0xFF for 8-bit data safety
    uint8_t receivedByte = (uint8_t)(huart->Instance->RDR & 0xFF);
    isrFlags = huart->Instance->ISR;

    HAL_UART_Transmit_IT(&huart4, &receivedByte, sizeof(_txData));
  }

  /*
  // Test ALWAYS sending "hello"
  //HAL_UART_Transmit_IT(&huart2, _txData, sizeof(_txData));
  if (HAL_UART_Transmit_IT(&huart4, _txData, sizeof(_txData)) == HAL_OK)
    _counterTxPass++;
  else
    _counterTxFail++;
  */

  /*
  UART_HandleTypeDef *huart = &huart2;
  isrFlags = huart->Instance->ISR;
  while ((isrFlags & USART_ISR_RXNE_RXFNE) != 0)
  {
    HAL_IWDG_Refresh(&hiwdg);
    uint8_t receivedByte = (uint8_t)(huart->Instance->RDR & 0xFF);

    // do something
    meh = receivedByte;
    receivedByte = meh;

    _counterRx++;
    isrFlags = huart->Instance->ISR;
  }
  */
  // TESTING END
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
  if (huart->Instance == UART4)
  {
    ////uint8_t *rxData = &huart->;
    // Restart interrupt-based reception
    HAL_UART_Receive_IT(&huart4, &_rxUart4, 1);

    ////HAL_UART_Receive_IT(&huart4, &rxData, 255);
    ////HAL_UART_Transmit_IT(&huart4, &rxData, sizeof(rxData));

    /*
    // TESTING START
    uint8_t meh = 0;
    uint32_t isrFlags = 0L;
    isrFlags = huart->Instance->ISR;
    while ((isrFlags & USART_ISR_RXNE_RXFNE) != 0)
    {
      uint8_t receivedByte = (uint8_t)(huart->Instance->RDR & 0xFF);

      // do something
      meh = receivedByte;
      receivedByte = meh;

      _counterRx++;
      isrFlags = huart->Instance->ISR;
    }
    // TESTING END

    _rxLastState = !_rxLastState;
    LedState(LED_DATA_ACTIVE, _rxLastState);

    bool isActive = PacketRxIsr();
    if (isActive)
      isActive = PacketTxIsr();

    // Check timer interrupt (~4.1ms) so we can trigger the
    // PacketBspRxTimerUpdate() and PacketBspBootloadTmrUpdate();
    if (!isActive)
    {
    }
    */
  }
}
```
